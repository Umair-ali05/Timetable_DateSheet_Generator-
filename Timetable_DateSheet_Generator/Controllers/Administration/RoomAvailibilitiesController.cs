using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timetable_DateSheet_Generator.Data.DbContext;
using Timetable_DateSheet_Generator.Data.Repositories.Account;
using Timetable_DateSheet_Generator.Data.Repositories.Building;
using Timetable_DateSheet_Generator.Data.Repositories.Department;
using Timetable_DateSheet_Generator.Data.Repositories.Institute;
using Timetable_DateSheet_Generator.Data.Repositories.Program;
using Timetable_DateSheet_Generator.Data.Repositories.ProgramRegularTiming;
using Timetable_DateSheet_Generator.Data.Repositories.ProgramSpecialTiming;
using Timetable_DateSheet_Generator.Data.Repositories.Room;
using Timetable_DateSheet_Generator.Data.Repositories.RoomAvailibility;
using Timetable_DateSheet_Generator.Models;
using Timetable_DateSheet_Generator.Models.ViewModels;

namespace Timetable_DateSheet_Generator.Controllers.Administration
{
    [Route("Administration/RoomAvailibilities/[action]")]
    [Authorize(Roles = "Administrator")]
    public class RoomAvailibilitiesController : Controller
    {
        private readonly AccountRepository accountRepository;
        private readonly InstituteRepository instituteRepository;
        private readonly BuildingRepository buildingRepository;
        private readonly RoomRepository roomRepository;
        private readonly DepartmentRepository departmentRepository;
        private readonly RoomAvailibilityRepository roomAvailibilityRepository;
        private readonly ProgramRepository programRepository;
        private readonly ProgramRegularTimingRepository programRegularTimingRepository;
        private readonly ProgramSpecialTimingRepository programSpecialTimingRepository;
        private static Departments department = new Departments();
        private static Rooms room = new Rooms();
        public RoomAvailibilitiesController(Timetable_DateSheet_Context timetable_DateSheet_Context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            accountRepository = new AccountRepository(timetable_DateSheet_Context, userManager, signInManager);
            instituteRepository = new InstituteRepository(timetable_DateSheet_Context);
            departmentRepository = new DepartmentRepository(timetable_DateSheet_Context);
            roomRepository = new RoomRepository(timetable_DateSheet_Context);
            roomAvailibilityRepository = new RoomAvailibilityRepository(timetable_DateSheet_Context);
            programRepository = new ProgramRepository(timetable_DateSheet_Context);
            programRegularTimingRepository = new ProgramRegularTimingRepository(timetable_DateSheet_Context);
            programSpecialTimingRepository = new ProgramSpecialTimingRepository(timetable_DateSheet_Context);
            buildingRepository = new BuildingRepository(timetable_DateSheet_Context);
        }
        public async Task<List<RoomAvailibilityViewModel>> GetList(List<Rooms> rooms)
        {
            var RoomAvailibilityViewModels = new List<RoomAvailibilityViewModel>();
            foreach (var room in rooms)
            {
                var range = await roomAvailibilityRepository.GetUniqueList(room);
                if (range == null || range.Count <= 0)
                    RoomAvailibilityViewModels.Add(new RoomAvailibilityViewModel() { Count = "No Timings Available.", Room = room, Department = new Departments() });
                else
                    RoomAvailibilityViewModels.AddRange(range);
            }
            return RoomAvailibilityViewModels;
        }
        [HttpGet]
        public async Task<IActionResult> Actions(string w_Form, string _Action, string Message, string MessageType, int? ID)
        {
            try
            {
                if (!string.IsNullOrEmpty(_Action) && _Action.Trim().ToLower().Contains("true")
                    && !string.IsNullOrEmpty(w_Form))
                {
                    if (w_Form.Trim().ToLower().Contains("create"))
                        ViewBag.Action = "Create";
                    else if (w_Form.Trim().ToLower().Contains("edit") && ID.HasValue)
                    {
                        ViewBag.ID = ID;
                        ViewBag.Action = "Edit";
                    }
                    else;
                }

                if (string.IsNullOrEmpty(Message))
                {
                    ViewBag.MessageType = Common.Success;
                    ViewBag.Message = Common.GetListSuccess;
                }
                else
                {
                    ViewBag.MessageType = MessageType;
                    ViewBag.Message = Message;
                }
                var list = roomRepository.GetAll(null);
                var result = await GetList(list.Result);
                return View(result);
            }
            catch
            {
                ViewBag.MessageType = Common.Error;
                ViewBag.Message = Common.GetListFail;
                return View(new List<Rooms>());
            }
        }
        [HttpGet]
        public IActionResult Create(string isNew)
        {
            if (!string.IsNullOrEmpty(isNew))
                ViewBag.Message = "Invalid Attempt";
            ViewData["Institutes"] = new SelectList(instituteRepository.GetForSelectList(), "ID", "Name");
            ViewData["Departments"] = new SelectList(departmentRepository.GetForSelectList(null), "ID", "Name");
            ViewData["Buildings"] = new SelectList(buildingRepository.GetForSelectList(null), "ID", "Name");
            ViewData["Rooms"] = new SelectList(roomRepository.GetForSelectList(null, null), "ID", "Name");
            return PartialView();
        }
        public DaySlots findDaySlot(List<Programs> programs, int Day)
        {
            DaySlots daySlots = new DaySlots();
            foreach (var program in programs)
            {
                var programRegularTimings = programRegularTimingRepository.GetByProgram(program.ProgramID);
                var programSpecialTimings = programSpecialTimingRepository.GetByProgram(program.ProgramID);
                for (int Hour = 0; Hour < 24; Hour++)
                {
                    var programRegularDay = programRegularTimings.Result.Where(c => c.Time.TimeWeekDay == Day).OrderBy(c => c.TimeID).ToList();
                    var programSpecialDay = programSpecialTimings.Result.Where(c => c.Time.TimeWeekDay == Day).OrderBy(c => c.TimeID).ToList();
                    foreach (var _Hour in programRegularDay)
                    {
                        var startTime = _Hour.Time.StartTime.ToString();
                        var hourID = "";
                        if (startTime.Length > 2)
                            hourID = startTime.Remove(startTime.Length - 2);
                        else
                            hourID = "0";
                        if (hourID == Convert.ToString(Hour))
                        {
                            if (!daySlots.isHourExists(Hour))
                                daySlots.Hours.Add(Hour);
                        }
                    }
                    foreach (var _Hour in programSpecialDay)
                    {
                        var startTime = _Hour.Time.StartTime.ToString();
                        var hourID = "";
                        if (startTime.Length > 2)
                            hourID = startTime.Remove(startTime.Length - 2);
                        else
                            hourID = "0";
                        if (hourID == Convert.ToString(Hour))
                        {
                            if (!daySlots.isHourExists(Hour))
                                daySlots.Hours.Add(Hour);
                        }
                    }

                }
            }
            return daySlots;
        }
        public DaySlots DayTimings(List<Programs> programs, int Day)
        {
            var findDaySlot = this.findDaySlot(programs, Day);
            DaySlots daySlots = new DaySlots(findDaySlot.Hours.Count)
            {
                Hours = findDaySlot.Hours
            };
            return daySlots;
        }
        [HttpGet]
        public async Task<IActionResult> LoadTimings(int? roomID, int? DepartmentID)
        {
            try
            {
                if (!roomID.HasValue || !DepartmentID.HasValue)
                    return RedirectToAction("Actions", new { MessageType = Common.Error, Message = "Not Found!" });
                if (await roomAvailibilityRepository.IsAnyRoomEntryExistAsync(roomID.Value, DepartmentID.Value))
                    return RedirectToAction("Actions", new { MessageType = Common.Error, Message = "Record Already Exists" });
                if (await roomRepository.IsExists(roomID.Value) && await departmentRepository.IsExists(DepartmentID.Value))
                {
                    room = roomRepository.GetById(roomID.Value).Result;
                    department = departmentRepository.GetById(DepartmentID.Value).Result;
                    var programs = programRepository.GetProgramsByDeparment(DepartmentID.Value);
                    TimingList timingList = new TimingList
                    {
                        MondaySlots = await Task.Run(() => DayTimings(programs.Result, 1)),
                        TuesdaySlots = await Task.Run(() => DayTimings(programs.Result, 2)),
                        WednesdaySlots = await Task.Run(() => DayTimings(programs.Result, 3)),
                        ThursdaySlots = await Task.Run(() => DayTimings(programs.Result, 4)),
                        FridaySlots = await Task.Run(() => DayTimings(programs.Result, 5)),
                        SaturdaySlots = await Task.Run(() => DayTimings(programs.Result, 6)),
                        SundaySlots = await Task.Run(() => DayTimings(programs.Result, 7))
                    };
                    return PartialView(timingList);
                }
            }
            catch { }
            return PartialView(new TimingList());
        }
        public List<bool> checkAllHours(int Day, List<bool> DaySlots, string Action, List<Times.HourSubSlots> startSlot, List<Times.HourSubSlots> endSlot, List<int> Hours)
        {
            for (int Hour = 0; Hour < Hours.Count; Hour++)
            {
                if (DaySlots[Hour] && Action == "New")
                    roomAvailibilityRepository.GenerateAndSave(Hours[Hour], Day, startSlot.ElementAt(Hour), endSlot.ElementAt(Hour), room.RoomID, department.DepartmentID);
                else if (Action == "Details")
                    DaySlots[Hour] = (roomAvailibilityRepository.checkSlots(Day, Hours[Hour], room.RoomID, department.DepartmentID));
                else
                    DaySlots[Hour] = false;
            }
            return DaySlots;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MondaySlots", "TuesdaySlots", "WednesdaySlots", "ThursdaySlots", "FridaySlots", "SaturdaySlots", "SundaySlots")]TimingList timingLists)
        {
            var msg = "";
            var msgType = "";
            try
            {
                if (room.Building.InstituteID != department.InstituteID)
                    return RedirectToAction("Actions", new { MessageType = Common.Error, Message = "Room and Department Must be in same institute." });
                if (ModelState.IsValid)
                {
                    await Task.Run(() => checkAllHours(1, timingLists.MondaySlots.Is_Hour_Slots, "New", timingLists.MondaySlots.Hour_Start_Slots, timingLists.MondaySlots.Hour_End_Slots, timingLists.MondaySlots.Hours));
                    await Task.Run(() => checkAllHours(2, timingLists.TuesdaySlots.Is_Hour_Slots, "New", timingLists.TuesdaySlots.Hour_Start_Slots, timingLists.TuesdaySlots.Hour_End_Slots, timingLists.TuesdaySlots.Hours));
                    await Task.Run(() => checkAllHours(3, timingLists.WednesdaySlots.Is_Hour_Slots, "New", timingLists.WednesdaySlots.Hour_Start_Slots, timingLists.WednesdaySlots.Hour_End_Slots, timingLists.WednesdaySlots.Hours));
                    await Task.Run(() => checkAllHours(4, timingLists.ThursdaySlots.Is_Hour_Slots, "New", timingLists.ThursdaySlots.Hour_Start_Slots, timingLists.ThursdaySlots.Hour_End_Slots, timingLists.ThursdaySlots.Hours));
                    await Task.Run(() => checkAllHours(5, timingLists.FridaySlots.Is_Hour_Slots, "New", timingLists.FridaySlots.Hour_Start_Slots, timingLists.FridaySlots.Hour_End_Slots, timingLists.FridaySlots.Hours));
                    await Task.Run(() => checkAllHours(6, timingLists.SaturdaySlots.Is_Hour_Slots, "New", timingLists.SaturdaySlots.Hour_Start_Slots, timingLists.SaturdaySlots.Hour_End_Slots, timingLists.SaturdaySlots.Hours));
                    await Task.Run(() => checkAllHours(7, timingLists.SundaySlots.Is_Hour_Slots, "New", timingLists.SundaySlots.Hour_Start_Slots, timingLists.SundaySlots.Hour_End_Slots, timingLists.SundaySlots.Hours));
                    await roomAvailibilityRepository.SaveChangesAsync();
                    roomAvailibilityRepository.SaveChanges();
                    msg = Common.InsertSuccess;
                    msgType = Common.Success;
                    department = null;
                    room = null;
                }
                else
                {
                    msg = Common.InsertFail;
                    msgType = Common.Fail;
                    department = null;
                    room = null;
                }
            }
            catch
            {
                msg = Common.Fail;
                msgType = Common.Error;
            }
            return RedirectToAction("Actions", new { MessageType = msgType, Message = msg });
        }
        public int getSlot(List<RoomAvailibilities> facultyTimings, int Day, int Hour, bool order)
        {
            var Days = facultyTimings.Where(c => c.Time.TimeWeekDay == Day);
            var id = Convert.ToString(Hour);
            List<RoomAvailibilities> Hours = new List<RoomAvailibilities>();
            foreach (var _Hour in Days)
            {
                var startTime = _Hour.Time.StartTime.ToString();
                var hourID = "";
                if (startTime.Length > 2)
                    hourID = startTime.Remove(startTime.Length - 2);
                else
                    hourID = "0";
                if (hourID == id)
                    Hours.Add(_Hour);
            }
            if (Days.Count() > 0 && Hours.Count() > 0)
            {
                if (order)
                {
                    var Slots = Hours.OrderBy(c => c.TimeID);
                    var value = Convert.ToString(Slots.ElementAt(0).TimeID);
                    return Convert.ToInt32(value.Substring(value.Length - 2));
                }
                else
                {
                    var Slots = Hours.OrderByDescending(c => c.TimeID);
                    var value = Convert.ToString(Slots.ElementAt(0).TimeID);
                    return Convert.ToInt32(value.Substring(value.Length - 2));
                }
            }
            else
                return 0;
        }
        public async Task<IActionResult> PreEdit(int? ID, int? Department)
        {
            if (ID.HasValue && await roomRepository.IsExists(ID.Value))
            {
                var _room = await roomRepository.GetById(ID.Value);
                var deptList = departmentRepository.GetForSelectList(_room.Building.InstituteID);
                if (Department.HasValue && Department.Value > 0)
                {
                    if (!await departmentRepository.IsExists(Department.Value))
                        return RedirectToAction("Actions", new { Message = Common.NotFound, MessageType = Common.Error });
                    var dept = await departmentRepository.GetById(Department.Value);
                    ViewData["Departments"] = new SelectList(deptList, "ID", "Name", dept.DepartmentID);
                    ViewBag.Flag = "y";
                    return PartialView(_room);
                }
                else
                {
                    ViewBag.Flag = "n";
                    ViewData["Departments"] = new SelectList(deptList, "ID", "Name");
                    return PartialView(_room);
                }
            }
            return RedirectToAction("Actions", new { Message = Common.NotFound, MessageType = Common.Error });
        }
        public List<Times.HourSubSlots> getAllSlotsValues(List<RoomAvailibilities> roomAvailibilities, bool order, List<int> Hours, int Day)
        {
            List<Times.HourSubSlots> Slots = new List<Times.HourSubSlots>();
            for (int Hour = 0; Hour < Hours.Count; Hour++)
                Slots.Add((Times.HourSubSlots)Enum.Parse(typeof(Times.HourSubSlots), Enum.GetName(typeof(Times.HourSubSlots), getSlot(roomAvailibilities, Day, Hours[Hour], order))));
            return Slots;
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string isValid, int? ID, int? Department)
        {
            var msgType = "";
            var msg = "";
            try
            {
                if (!string.IsNullOrEmpty(isValid))
                    ViewBag.Message = "Invalid attempt.";

                if (ID.HasValue && await roomRepository.IsExists(ID.Value) && Department.HasValue && await departmentRepository.IsExists(Department.Value))
                {
                    room = await roomRepository.GetById(ID.Value);
                    department = await departmentRepository.GetById(Department.Value);
                    TimingList timingLists = new TimingList();
                    var programs = await programRepository.GetProgramsByDeparment(department.DepartmentID);
                    timingLists.MondaySlots = await Task.Run(() => DayTimings(programs, 1));
                    timingLists.TuesdaySlots = await Task.Run(() => DayTimings(programs, 2));
                    timingLists.WednesdaySlots = await Task.Run(() => DayTimings(programs, 3));
                    timingLists.ThursdaySlots = await Task.Run(() => DayTimings(programs, 4));
                    timingLists.FridaySlots = await Task.Run(() => DayTimings(programs, 5));
                    timingLists.SaturdaySlots = await Task.Run(() => DayTimings(programs, 6));
                    timingLists.SundaySlots = await Task.Run(() => DayTimings(programs, 7));
                    var roomAvailibilities = await roomAvailibilityRepository.GetByRoomDepartment(room.RoomID, department.DepartmentID);
                    timingLists.MondaySlots.Is_Hour_Slots = await Task.Run(() => checkAllHours(1, timingLists.MondaySlots.Is_Hour_Slots, "Details", timingLists.MondaySlots.Hour_Start_Slots, timingLists.MondaySlots.Hour_End_Slots, timingLists.MondaySlots.Hours));
                    timingLists.MondaySlots.Hour_End_Slots = await Task.Run(() => getAllSlotsValues(roomAvailibilities, false, timingLists.MondaySlots.Hours, 1));
                    timingLists.MondaySlots.Hour_Start_Slots = await Task.Run(() => getAllSlotsValues(roomAvailibilities, true, timingLists.MondaySlots.Hours, 1));
                    timingLists.TuesdaySlots.Is_Hour_Slots = await Task.Run(() => checkAllHours(2, timingLists.TuesdaySlots.Is_Hour_Slots, "Details", timingLists.TuesdaySlots.Hour_Start_Slots, timingLists.TuesdaySlots.Hour_End_Slots, timingLists.TuesdaySlots.Hours));
                    timingLists.TuesdaySlots.Hour_Start_Slots = await Task.Run(() => getAllSlotsValues(roomAvailibilities, true, timingLists.TuesdaySlots.Hours, 2));
                    timingLists.TuesdaySlots.Hour_End_Slots = await Task.Run(() => getAllSlotsValues(roomAvailibilities, false, timingLists.TuesdaySlots.Hours, 2));
                    timingLists.WednesdaySlots.Is_Hour_Slots = await Task.Run(() => checkAllHours(3, timingLists.WednesdaySlots.Is_Hour_Slots, "Details", timingLists.WednesdaySlots.Hour_Start_Slots, timingLists.WednesdaySlots.Hour_End_Slots, timingLists.WednesdaySlots.Hours));
                    timingLists.WednesdaySlots.Hour_Start_Slots = await Task.Run(() => getAllSlotsValues(roomAvailibilities, true, timingLists.WednesdaySlots.Hours, 3));
                    timingLists.WednesdaySlots.Hour_End_Slots = await Task.Run(() => getAllSlotsValues(roomAvailibilities, false, timingLists.WednesdaySlots.Hours, 3));
                    timingLists.ThursdaySlots.Is_Hour_Slots = await Task.Run(() => checkAllHours(4, timingLists.ThursdaySlots.Is_Hour_Slots, "Details", timingLists.ThursdaySlots.Hour_Start_Slots, timingLists.ThursdaySlots.Hour_End_Slots, timingLists.ThursdaySlots.Hours));
                    timingLists.ThursdaySlots.Hour_Start_Slots = await Task.Run(() => getAllSlotsValues(roomAvailibilities, true, timingLists.ThursdaySlots.Hours, 4));
                    timingLists.ThursdaySlots.Hour_End_Slots = await Task.Run(() => getAllSlotsValues(roomAvailibilities, false, timingLists.ThursdaySlots.Hours, 4));
                    timingLists.FridaySlots.Is_Hour_Slots = await Task.Run(() => checkAllHours(5, timingLists.FridaySlots.Is_Hour_Slots, "Details", timingLists.FridaySlots.Hour_Start_Slots, timingLists.FridaySlots.Hour_End_Slots, timingLists.FridaySlots.Hours));
                    timingLists.FridaySlots.Hour_Start_Slots = await Task.Run(() => getAllSlotsValues(roomAvailibilities, true, timingLists.FridaySlots.Hours, 5));
                    timingLists.FridaySlots.Hour_End_Slots = await Task.Run(() => getAllSlotsValues(roomAvailibilities, false, timingLists.FridaySlots.Hours, 5));
                    timingLists.SaturdaySlots.Is_Hour_Slots = await Task.Run(() => checkAllHours(6, timingLists.SaturdaySlots.Is_Hour_Slots, "Details", timingLists.SaturdaySlots.Hour_Start_Slots, timingLists.SaturdaySlots.Hour_End_Slots, timingLists.SaturdaySlots.Hours));
                    timingLists.SaturdaySlots.Hour_Start_Slots = await Task.Run(() => getAllSlotsValues(roomAvailibilities, true, timingLists.SaturdaySlots.Hours, 6));
                    timingLists.SaturdaySlots.Hour_End_Slots = await Task.Run(() => getAllSlotsValues(roomAvailibilities, false, timingLists.SaturdaySlots.Hours, 6));
                    timingLists.SundaySlots.Is_Hour_Slots = await Task.Run(() => checkAllHours(7, timingLists.SundaySlots.Is_Hour_Slots, "Details", timingLists.SundaySlots.Hour_Start_Slots, timingLists.SundaySlots.Hour_End_Slots, timingLists.SundaySlots.Hours));
                    timingLists.SundaySlots.Hour_Start_Slots = await Task.Run(() => getAllSlotsValues(roomAvailibilities, true, timingLists.SundaySlots.Hours, 7));
                    timingLists.SundaySlots.Hour_End_Slots = await Task.Run(() => getAllSlotsValues(roomAvailibilities, false, timingLists.SundaySlots.Hours, 7));
                    return PartialView(timingLists);
                }
                else
                {
                    msgType = Common.Error;
                    msg = Common.NotFound;
                }
            }
            catch (Exception ex)
            {
                msgType = Common.Error;
                msg = Common.Fail;
            }

            return RedirectToAction("Actions", new { MessageType = msgType, Message = msg });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("MondaySlots", "TuesdaySlots", "WednesdaySlots", "ThursdaySlots", "FridaySlots", "SaturdaySlots", "SundaySlots")]TimingList timingLists)
        {
            var msg = "";
            var msgtype = "";
            try
            {
                if (ModelState.IsValid)
                {
                    await roomAvailibilityRepository.RemoveRange(room.RoomID, department.DepartmentID);
                    await roomAvailibilityRepository.SaveChangesAsync();
                    timingLists.MondaySlots.Is_Hour_Slots = await Task.Run(() => checkAllHours(1, timingLists.MondaySlots.Is_Hour_Slots, "New", timingLists.MondaySlots.Hour_Start_Slots, timingLists.MondaySlots.Hour_End_Slots, timingLists.MondaySlots.Hours));
                    timingLists.TuesdaySlots.Is_Hour_Slots = await Task.Run(() => checkAllHours(2, timingLists.TuesdaySlots.Is_Hour_Slots, "New", timingLists.TuesdaySlots.Hour_Start_Slots, timingLists.TuesdaySlots.Hour_End_Slots, timingLists.TuesdaySlots.Hours));
                    timingLists.WednesdaySlots.Is_Hour_Slots = await Task.Run(() => checkAllHours(3, timingLists.WednesdaySlots.Is_Hour_Slots, "New", timingLists.WednesdaySlots.Hour_Start_Slots, timingLists.WednesdaySlots.Hour_End_Slots, timingLists.WednesdaySlots.Hours));
                    timingLists.ThursdaySlots.Is_Hour_Slots = await Task.Run(() => checkAllHours(4, timingLists.ThursdaySlots.Is_Hour_Slots, "New", timingLists.ThursdaySlots.Hour_Start_Slots, timingLists.ThursdaySlots.Hour_End_Slots, timingLists.ThursdaySlots.Hours));
                    timingLists.FridaySlots.Is_Hour_Slots = await Task.Run(() => checkAllHours(5, timingLists.FridaySlots.Is_Hour_Slots, "New", timingLists.FridaySlots.Hour_Start_Slots, timingLists.FridaySlots.Hour_End_Slots, timingLists.FridaySlots.Hours));
                    timingLists.SaturdaySlots.Is_Hour_Slots = await Task.Run(() => checkAllHours(6, timingLists.SaturdaySlots.Is_Hour_Slots, "New", timingLists.SaturdaySlots.Hour_Start_Slots, timingLists.SaturdaySlots.Hour_End_Slots, timingLists.SaturdaySlots.Hours));
                    timingLists.SundaySlots.Is_Hour_Slots = await Task.Run(() => checkAllHours(7, timingLists.SundaySlots.Is_Hour_Slots, "New", timingLists.SundaySlots.Hour_Start_Slots, timingLists.SundaySlots.Hour_End_Slots, timingLists.SundaySlots.Hours));
                    msg = Common.UpdateSuccess;
                    msgtype = Common.Success;
                }
                else
                {
                    msg = Common.UpdateFail;
                    msgtype = Common.Error;
                }
            }
            catch (Exception)
            {
                msg = Common.Fail;
                msgtype = Common.Error;
            }
            room = null;
            department = null;
            return RedirectToAction("Actions", new { Message = msg, MessageType = msgtype });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? ID, int? Department)
        {
            var msgType = "";
            var msg = "";
            try
            {
                if (ID.HasValue && Department.HasValue)
                {
                    var room = await roomRepository.GetById(ID.Value);
                    var dept = await departmentRepository.GetById(Department.Value);
                    var count = roomAvailibilityRepository.getCount(ID.Value, Department.Value);
                    return PartialView(new RoomAvailibilityViewModel() { Room = room, Department = dept, Count = count > 0 ? count.ToString() : "No Timings Available." });
                }
                msgType = Common.Error;
                msg = Common.NotFound;
            }
            catch
            {
                msgType = Common.Error;
                msg = Common.Fail;
            }
            return RedirectToAction("Actions", new { MessageType = msgType, Message = msg });
        }
        [HttpPost]
        public async Task<IActionResult> Delete(RoomAvailibilityViewModel availibilityViewModel)
        {
            var msgType = "";
            var msg = "";
            try
            {
                if (await (roomAvailibilityRepository.IsAnyRoomEntryExistAsync(availibilityViewModel.Room.RoomID, availibilityViewModel.Department.DepartmentID)))
                {
                    await roomAvailibilityRepository.RemoveRange(availibilityViewModel.Room.RoomID, availibilityViewModel.Department.DepartmentID);
                    await roomAvailibilityRepository.SaveChangesAsync();
                    msgType = Common.Success;
                    msg = Common.DeleteSuccess;
                }
                else
                {
                    msgType = Common.Error;
                    msg = Common.NotFound;
                }
            }
            catch (Exception ex)
            {
                msgType = Common.Error;
                msg = Common.Fail;
            }
            return RedirectToAction("Actions", new { MessageType = msgType, Message = msg });
        }
        public JsonResult getDepartments(string institute)
        {
            if (!string.IsNullOrEmpty(institute))
                return Json(departmentRepository.GetForSelectList(Convert.ToInt32(institute)));
            else
                return Json(departmentRepository.GetForSelectList(null));
        }
        public JsonResult getBuildings(string institute)
        {
            return Json(buildingRepository.GetForSelectList(string.IsNullOrEmpty(institute) ? (int?)null : Convert.ToInt32(institute)));
        }
        public JsonResult getRooms(string institute, string Building)
        {
            return Json(roomRepository.GetForSelectList(string.IsNullOrEmpty(institute) ? (int?)null : Convert.ToInt32(institute), string.IsNullOrEmpty(Building) ? (int?)null : Convert.ToInt32(Building)));
        }
    }
}