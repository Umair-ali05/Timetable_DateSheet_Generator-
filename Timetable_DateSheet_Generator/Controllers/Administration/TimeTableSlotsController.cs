using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Timetable_DateSheet_Generator.Data.DbContext;
using Timetable_DateSheet_Generator.Data.Repositories.Account;
using Timetable_DateSheet_Generator.Data.Repositories.Department;
using Timetable_DateSheet_Generator.Data.Repositories.Institute;
using Timetable_DateSheet_Generator.Data.Repositories.Program;
using Timetable_DateSheet_Generator.Data.Repositories.ProgramRegularTiming;
using Timetable_DateSheet_Generator.Data.Repositories.ProgramSpecialTiming;
using Timetable_DateSheet_Generator.Data.Repositories.Semester;
using Timetable_DateSheet_Generator.Data.Repositories.TimeTable;
using Timetable_DateSheet_Generator.Data.Repositories.TimeTableTimeSlot;
using Timetable_DateSheet_Generator.Models;
using Timetable_DateSheet_Generator.Models.ViewModels;

namespace Timetable_DateSheet_Generator.Controllers.Administration
{
    [Route("Administration/TimeTableSlots/[action]")]
    [Authorize(Roles = "Administrator")]
    public class TimeTableSlotsController : Controller
    {
        private readonly AccountRepository accountRepository;
        private readonly InstituteRepository instituteRepository;
        private readonly TimeTableRepository timeTableRepository;
        private readonly DepartmentRepository departmentRepository;
        private readonly SemesterRepository semesterRepository;
        private readonly TimeTableTimings timeTableTimings;
        private readonly ProgramRepository programRepository;
        private readonly ProgramRegularTimingRepository programRegularTimingRepository;
        private readonly ProgramSpecialTimingRepository programSpecialTimingRepository;
        private static TimeTables timeTable = new TimeTables();
        public TimeTableSlotsController(Timetable_DateSheet_Context timetable_DateSheet_Context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            accountRepository = new AccountRepository(timetable_DateSheet_Context, userManager, signInManager);
            instituteRepository = new InstituteRepository(timetable_DateSheet_Context);
            departmentRepository = new DepartmentRepository(timetable_DateSheet_Context);
            timeTableRepository = new TimeTableRepository(timetable_DateSheet_Context);
            timeTableTimings = new TimeTableTimings(timetable_DateSheet_Context);
            programRepository = new ProgramRepository(timetable_DateSheet_Context);
            programRegularTimingRepository = new ProgramRegularTimingRepository(timetable_DateSheet_Context);
            programSpecialTimingRepository = new ProgramSpecialTimingRepository(timetable_DateSheet_Context);
            semesterRepository = new SemesterRepository(timetable_DateSheet_Context);
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
                var list = await timeTableRepository.GetAll(null);
                List<string> timings = new List<string>();
                foreach (var item in list)
                {
                    var count = timeTableTimings.getCount(item.TimeTableID);
                    if (count > 0)
                        timings.Add(count.ToString() + " Slots.");
                    else
                        timings.Add("No Timings Available.");
                }
                ViewData["Timings"] = timings;
                return View(list);
            }
            catch
            {
                ViewBag.MessageType = Common.Error;
                ViewBag.Message = Common.GetListFail;
                return View(new List<TimeTables>());
            }
        }
        [HttpGet]
        public IActionResult Create(string isNew)
        {
            if (!string.IsNullOrEmpty(isNew))
                ViewBag.Message = "Invalid Attempt";
            ViewData["Institutes"] = new SelectList(instituteRepository.GetForSelectList(), "ID", "Name");
            ViewData["Semesters"] = new SelectList(semesterRepository.GetForSelectList(), "ID", "Name");
            ViewData["TimeTables"] = new SelectList(timeTableRepository.GetForSelectList(null, null), "ID", "Name");
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
        public async Task<IActionResult> LoadTimings(int timeTableID)
        {
            try
            {
                if (await timeTableRepository.IsExists(timeTableID))
                {
                    if (await timeTableTimings.IsAnytimeTableEntryExistAsync(timeTableID))
                        return RedirectToAction("Actions", new { MesssageType = Common.Error, Message = "Record Already Exists" });
                    timeTable = timeTableRepository.GetById(timeTableID).Result;
                    var programs = new List<Programs>();
                    foreach (var dept in departmentRepository.GetByInstitute(timeTable.InstituteID).Result)
                        programs.AddRange(programRepository.GetProgramsByDeparment(dept.DepartmentID).Result);
                    TimingList timingList = new TimingList
                    {
                        MondaySlots = await Task.Run(() => DayTimings(programs, 1)),
                        TuesdaySlots = await Task.Run(() => DayTimings(programs, 2)),
                        WednesdaySlots = await Task.Run(() => DayTimings(programs, 3)),
                        ThursdaySlots = await Task.Run(() => DayTimings(programs, 4)),
                        FridaySlots = await Task.Run(() => DayTimings(programs, 5)),
                        SaturdaySlots = await Task.Run(() => DayTimings(programs, 6)),
                        SundaySlots = await Task.Run(() => DayTimings(programs, 7))
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
                    timeTableTimings.GenerateAndSave(Hours[Hour], Day, startSlot.ElementAt(Hour), endSlot.ElementAt(Hour), timeTable.TimeTableID);
                else if (Action == "Details")
                    DaySlots[Hour] = (timeTableTimings.checkSlots(Day, Hours[Hour], timeTable.TimeTableID));
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
                if (ModelState.IsValid)
                {
                    await Task.Run(() => checkAllHours(1, timingLists.MondaySlots.Is_Hour_Slots, "New", timingLists.MondaySlots.Hour_Start_Slots, timingLists.MondaySlots.Hour_End_Slots, timingLists.MondaySlots.Hours));
                    await Task.Run(() => checkAllHours(2, timingLists.TuesdaySlots.Is_Hour_Slots, "New", timingLists.TuesdaySlots.Hour_Start_Slots, timingLists.TuesdaySlots.Hour_End_Slots, timingLists.TuesdaySlots.Hours));
                    await Task.Run(() => checkAllHours(3, timingLists.WednesdaySlots.Is_Hour_Slots, "New", timingLists.WednesdaySlots.Hour_Start_Slots, timingLists.WednesdaySlots.Hour_End_Slots, timingLists.WednesdaySlots.Hours));
                    await Task.Run(() => checkAllHours(4, timingLists.ThursdaySlots.Is_Hour_Slots, "New", timingLists.ThursdaySlots.Hour_Start_Slots, timingLists.ThursdaySlots.Hour_End_Slots, timingLists.ThursdaySlots.Hours));
                    await Task.Run(() => checkAllHours(5, timingLists.FridaySlots.Is_Hour_Slots, "New", timingLists.FridaySlots.Hour_Start_Slots, timingLists.FridaySlots.Hour_End_Slots, timingLists.FridaySlots.Hours));
                    await Task.Run(() => checkAllHours(6, timingLists.SaturdaySlots.Is_Hour_Slots, "New", timingLists.SaturdaySlots.Hour_Start_Slots, timingLists.SaturdaySlots.Hour_End_Slots, timingLists.SaturdaySlots.Hours));
                    await Task.Run(() => checkAllHours(7, timingLists.SundaySlots.Is_Hour_Slots, "New", timingLists.SundaySlots.Hour_Start_Slots, timingLists.SundaySlots.Hour_End_Slots, timingLists.SundaySlots.Hours));
                    await timeTableTimings.SaveChangesAsync();
                    timeTableTimings.SaveChanges();
                    msg = Common.InsertSuccess;
                    msgType = Common.Success;
                }
                else
                {
                    msg = Common.InsertFail;
                    msgType = Common.Fail;
                    timeTable = null;

                }
            }
            catch
            {
                msg = Common.Fail;
                msgType = Common.Error;
            }
            timeTable = null;
            return RedirectToAction("Actions", new { MessageType = msg, Message = msgType });
        }
        public int getSlot(List<TimeSlots> timeTableTimings, int Day, int Hour, bool order)
        {
            var Days = timeTableTimings.Where(c => c.Time.TimeWeekDay == Day);
            var id = Convert.ToString(Hour);
            List<TimeSlots> Hours = new List<TimeSlots>();
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
        public async Task<IActionResult> PreEdit(int? ID)
        {
            if (ID.HasValue && await timeTableRepository.IsExists(ID.Value))
            {
                return PartialView(await timeTableRepository.GetById(ID.Value));
            }
            return RedirectToAction("Actions", new { Message = Common.NotFound, MessageType = Common.Error });
        }
        public List<Times.HourSubSlots> getAllSlotsValues(List<TimeSlots> timeSlots, bool order, List<int> Hours, int Day)
        {
            List<Times.HourSubSlots> Slots = new List<Times.HourSubSlots>();
            for (int Hour = 0; Hour < Hours.Count; Hour++)
                Slots.Add((Times.HourSubSlots)Enum.Parse(typeof(Times.HourSubSlots), Enum.GetName(typeof(Times.HourSubSlots), getSlot(timeSlots, Day, Hours[Hour], order))));
            return Slots;
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string isValid, int? ID)
        {
            var msgType = "";
            var msg = "";
            try
            {
                if (!string.IsNullOrEmpty(isValid))
                    ViewBag.Message = "Invalid attempt.";

                if (ID.HasValue && await timeTableRepository.IsExists(ID.Value))
                {
                    timeTable = await timeTableRepository.GetById(ID.Value);
                    TimingList timingLists = new TimingList();
                    var programs = new List<Programs>();
                    foreach (var dept in departmentRepository.GetByInstitute(timeTable.InstituteID).Result)
                        programs.AddRange(programRepository.GetProgramsByDeparment(dept.DepartmentID).Result);
                    timingLists.MondaySlots = await Task.Run(() => DayTimings(programs, 1));
                    timingLists.TuesdaySlots = await Task.Run(() => DayTimings(programs, 2));
                    timingLists.WednesdaySlots = await Task.Run(() => DayTimings(programs, 3));
                    timingLists.ThursdaySlots = await Task.Run(() => DayTimings(programs, 4));
                    timingLists.FridaySlots = await Task.Run(() => DayTimings(programs, 5));
                    timingLists.SaturdaySlots = await Task.Run(() => DayTimings(programs, 6));
                    timingLists.SundaySlots = await Task.Run(() => DayTimings(programs, 7));
                    var timetableTimings = await timeTableTimings.GetByTimetable(timeTable.TimeTableID);
                    timingLists.MondaySlots.Is_Hour_Slots = await Task.Run(() => checkAllHours(1, timingLists.MondaySlots.Is_Hour_Slots, "Details", timingLists.MondaySlots.Hour_Start_Slots, timingLists.MondaySlots.Hour_End_Slots, timingLists.MondaySlots.Hours));
                    timingLists.MondaySlots.Hour_End_Slots = await Task.Run(() => getAllSlotsValues(timetableTimings, false, timingLists.MondaySlots.Hours, 1));
                    timingLists.MondaySlots.Hour_Start_Slots = await Task.Run(() => getAllSlotsValues(timetableTimings, true, timingLists.MondaySlots.Hours, 1));
                    timingLists.TuesdaySlots.Is_Hour_Slots = await Task.Run(() => checkAllHours(2, timingLists.TuesdaySlots.Is_Hour_Slots, "Details", timingLists.TuesdaySlots.Hour_Start_Slots, timingLists.TuesdaySlots.Hour_End_Slots, timingLists.TuesdaySlots.Hours));
                    timingLists.TuesdaySlots.Hour_Start_Slots = await Task.Run(() => getAllSlotsValues(timetableTimings, true, timingLists.TuesdaySlots.Hours, 2));
                    timingLists.TuesdaySlots.Hour_End_Slots = await Task.Run(() => getAllSlotsValues(timetableTimings, false, timingLists.TuesdaySlots.Hours, 2));
                    timingLists.WednesdaySlots.Is_Hour_Slots = await Task.Run(() => checkAllHours(3, timingLists.WednesdaySlots.Is_Hour_Slots, "Details", timingLists.WednesdaySlots.Hour_Start_Slots, timingLists.WednesdaySlots.Hour_End_Slots, timingLists.WednesdaySlots.Hours));
                    timingLists.WednesdaySlots.Hour_Start_Slots = await Task.Run(() => getAllSlotsValues(timetableTimings, true, timingLists.WednesdaySlots.Hours, 3));
                    timingLists.WednesdaySlots.Hour_End_Slots = await Task.Run(() => getAllSlotsValues(timetableTimings, false, timingLists.WednesdaySlots.Hours, 3));
                    timingLists.ThursdaySlots.Is_Hour_Slots = await Task.Run(() => checkAllHours(4, timingLists.ThursdaySlots.Is_Hour_Slots, "Details", timingLists.ThursdaySlots.Hour_Start_Slots, timingLists.ThursdaySlots.Hour_End_Slots, timingLists.ThursdaySlots.Hours));
                    timingLists.ThursdaySlots.Hour_Start_Slots = await Task.Run(() => getAllSlotsValues(timetableTimings, true, timingLists.ThursdaySlots.Hours, 4));
                    timingLists.ThursdaySlots.Hour_End_Slots = await Task.Run(() => getAllSlotsValues(timetableTimings, false, timingLists.ThursdaySlots.Hours, 4));
                    timingLists.FridaySlots.Is_Hour_Slots = await Task.Run(() => checkAllHours(5, timingLists.FridaySlots.Is_Hour_Slots, "Details", timingLists.FridaySlots.Hour_Start_Slots, timingLists.FridaySlots.Hour_End_Slots, timingLists.FridaySlots.Hours));
                    timingLists.FridaySlots.Hour_Start_Slots = await Task.Run(() => getAllSlotsValues(timetableTimings, true, timingLists.FridaySlots.Hours, 5));
                    timingLists.FridaySlots.Hour_End_Slots = await Task.Run(() => getAllSlotsValues(timetableTimings, false, timingLists.FridaySlots.Hours, 5));
                    timingLists.SaturdaySlots.Is_Hour_Slots = await Task.Run(() => checkAllHours(6, timingLists.SaturdaySlots.Is_Hour_Slots, "Details", timingLists.SaturdaySlots.Hour_Start_Slots, timingLists.SaturdaySlots.Hour_End_Slots, timingLists.SaturdaySlots.Hours));
                    timingLists.SaturdaySlots.Hour_Start_Slots = await Task.Run(() => getAllSlotsValues(timetableTimings, true, timingLists.SaturdaySlots.Hours, 6));
                    timingLists.SaturdaySlots.Hour_End_Slots = await Task.Run(() => getAllSlotsValues(timetableTimings, false, timingLists.SaturdaySlots.Hours, 6));
                    timingLists.SundaySlots.Is_Hour_Slots = await Task.Run(() => checkAllHours(7, timingLists.SundaySlots.Is_Hour_Slots, "Details", timingLists.SundaySlots.Hour_Start_Slots, timingLists.SundaySlots.Hour_End_Slots, timingLists.SundaySlots.Hours));
                    timingLists.SundaySlots.Hour_Start_Slots = await Task.Run(() => getAllSlotsValues(timetableTimings, true, timingLists.SundaySlots.Hours, 7));
                    timingLists.SundaySlots.Hour_End_Slots = await Task.Run(() => getAllSlotsValues(timetableTimings, false, timingLists.SundaySlots.Hours, 7));
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
                    await timeTableTimings.RemoveRange(timeTable.TimeTableID);
                    await timeTableTimings.SaveChangesAsync();
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
            catch (Exception ex)
            {
                msg = Common.Fail;
                msgtype = Common.Error;
            }
            timeTable = null;
            return RedirectToAction("Actions", new { Message = msg, MessageType = msgtype });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? ID)
        {
            var msgType = "";
            var msg = "";
            try
            {
                if (ID.HasValue)
                {
                    var timeTable = await timeTableRepository.GetById(ID.Value);
                    var user = accountRepository.GetUserByID(timeTable.User);
                    ViewBag.Name = user.Name;
                    ViewBag.Email = user.Email;
                    ViewBag.TotalTimings = timeTableTimings.getCount(ID.Value).ToString();
                    return PartialView(timeTable);
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
        public async Task<IActionResult> Delete(TimeTables timeTable)
        {
            var msgType = "";
            var msg = "";
            try
            {
                using (var ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    if (await (timeTableTimings.IsAnytimeTableEntryExistAsync(timeTable.TimeTableID)))
                    {
                        await timeTableTimings.RemoveRange(timeTable.TimeTableID);
                        await timeTableTimings.SaveChangesAsync();
                        msgType = Common.Success;
                        msg = Common.DeleteSuccess;
                    }
                    else
                    {
                        msgType = Common.Error;
                        msg = Common.NotFound;
                    }
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                msgType = Common.Error;
                msg = Common.Fail;
            }
            return RedirectToAction("Actions", new { MessageType = msgType, Message = msg });
        }
        public JsonResult getTimetables(string institute, string semester)
        {
            return Json(timeTableRepository.GetForSelectList(string.IsNullOrEmpty(institute) ? (int?)null : Convert.ToInt32(institute), string.IsNullOrEmpty(semester) ? (int?)null : Convert.ToInt32(semester)));
        }
    }
}