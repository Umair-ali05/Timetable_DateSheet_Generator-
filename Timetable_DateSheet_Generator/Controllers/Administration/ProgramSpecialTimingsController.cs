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
using Timetable_DateSheet_Generator.Data.Repositories.Department;
using Timetable_DateSheet_Generator.Data.Repositories.Institute;
using Timetable_DateSheet_Generator.Data.Repositories.Program;
using Timetable_DateSheet_Generator.Data.Repositories.ProgramSpecialTiming;
using Timetable_DateSheet_Generator.Models;
using Timetable_DateSheet_Generator.Models.ViewModels;

namespace Timetable_DateSheet_Generator.Controllers.Administration
{
    [Route("Administration/ProgramSpecialTimings/[action]")]
    [Authorize(Roles = "Administrator")]
    public class ProgramSpecialTimingsController : Controller
    {
        private readonly AccountRepository accountRepository;
        private readonly InstituteRepository instituteRepository;
        private readonly ProgramRepository programRepository;
        private readonly DepartmentRepository departmentRepository;
        private readonly ProgramSpecialTimingRepository programSpecialTimingRepository;
        private static Programs program = new Programs();
        public ProgramSpecialTimingsController(Timetable_DateSheet_Context timetable_DateSheet_Context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            accountRepository = new AccountRepository(timetable_DateSheet_Context, userManager, signInManager);
            instituteRepository = new InstituteRepository(timetable_DateSheet_Context);
            departmentRepository = new DepartmentRepository(timetable_DateSheet_Context);
            programRepository = new ProgramRepository(timetable_DateSheet_Context);
            programSpecialTimingRepository = new ProgramSpecialTimingRepository(timetable_DateSheet_Context);
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
                var list = await programRepository.GetAll(null);
                List<string> timings = new List<string>();
                foreach (var item in list)
                {
                    var count = programSpecialTimingRepository.getCount(item.ProgramID);
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
                return View(new List<Programs>());
            }
        }
        [HttpGet]
        public IActionResult Create(string isNew)
        {
            if (!string.IsNullOrEmpty(isNew))
                ViewBag.Message = "Invalid Attempt";
            ViewData["Institutes"] = new SelectList(instituteRepository.GetForSelectList(), "ID", "Name");
            ViewData["Departments"] = new SelectList(departmentRepository.GetForSelectList(null), "ID", "Name");
            ViewData["Programs"] = new SelectList(programRepository.GetForSelectList(null, null, true), "ID", "Name");
            return PartialView();
        }
        [HttpGet]
        public async Task<IActionResult> LoadTimings(int ProgramID)
        {
            try
            {
                if (await programRepository.IsExists(ProgramID))
                {
                    if (await programSpecialTimingRepository.IsAnyProgramEntryExistAsync(ProgramID))
                        return RedirectToAction("Actions", new { MesssageType = Common.Error, Message = "Record Already Exists" });
                    TimingList timingList = new TimingList(24, 24, 24, 24, 24, 24, 24);
                    program = await programRepository.GetById(ProgramID);
                    return PartialView(timingList);
                }
            }
            catch { }
            return PartialView(new TimingList());
        }
        public List<bool> checkAllHours(int Day, List<bool> DaySlots, string Action, List<Times.HourSubSlots> startSlot, List<Times.HourSubSlots> endSlot)
        {
            for (int Hour = 0; Hour < DaySlots.Count; Hour++)
            {
                if (DaySlots[Hour] && Action == "New")
                    programSpecialTimingRepository.GenerateAndSave(Hour, Day, startSlot.ElementAt(Hour), endSlot.ElementAt(Hour), program.ProgramID);
                else if (Action == "Details")
                    DaySlots[Hour] = (programSpecialTimingRepository.checkSlots(Day, Hour, program.ProgramID));
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
                    checkAllHours(1, timingLists.MondaySlots.Is_Hour_Slots, "New", timingLists.MondaySlots.Hour_Start_Slots, timingLists.MondaySlots.Hour_End_Slots);
                    checkAllHours(2, timingLists.TuesdaySlots.Is_Hour_Slots, "New", timingLists.TuesdaySlots.Hour_Start_Slots, timingLists.TuesdaySlots.Hour_End_Slots);
                    checkAllHours(3, timingLists.WednesdaySlots.Is_Hour_Slots, "New", timingLists.WednesdaySlots.Hour_Start_Slots, timingLists.WednesdaySlots.Hour_End_Slots);
                    checkAllHours(4, timingLists.ThursdaySlots.Is_Hour_Slots, "New", timingLists.ThursdaySlots.Hour_Start_Slots, timingLists.ThursdaySlots.Hour_End_Slots);
                    checkAllHours(5, timingLists.FridaySlots.Is_Hour_Slots, "New", timingLists.FridaySlots.Hour_Start_Slots, timingLists.FridaySlots.Hour_End_Slots);
                    checkAllHours(6, timingLists.SaturdaySlots.Is_Hour_Slots, "New", timingLists.SaturdaySlots.Hour_Start_Slots, timingLists.SaturdaySlots.Hour_End_Slots);
                    checkAllHours(7, timingLists.SundaySlots.Is_Hour_Slots, "New", timingLists.SundaySlots.Hour_Start_Slots, timingLists.SundaySlots.Hour_End_Slots);
                    program = null;
                    await programSpecialTimingRepository.SaveChangesAsync();
                    msg = Common.InsertSuccess;
                    msgType = Common.Success;
                }
                else
                {
                    msg = Common.InsertFail;
                    msgType = Common.Fail;
                    program = null;

                }
            }
            catch
            {
                msg = Common.Fail;
                msgType = Common.Error;
            }
            return RedirectToAction("Actions", new { MessageType = msg, Message = msgType });
        }
        public int getSlot(List<ProgramSpecialTimings> programSpecialTimings, int Day, int Hour, bool order)
        {
            var Days = programSpecialTimings.Where(c => c.Time.TimeWeekDay == Day);
            var id = Convert.ToString(Hour);
            List<ProgramSpecialTimings> Hours = new List<ProgramSpecialTimings>();
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
            if (ID.HasValue && await programRepository.IsExists(ID.Value))
            {
                return PartialView(await programRepository.GetById(ID.Value));
            }
            return RedirectToAction("Actions", new { Message = Common.NotFound, MessageType = Common.Error });
        }
        public List<Times.HourSubSlots> getAllSlotsValues(List<ProgramSpecialTimings> programSpecialTimings, bool order, int length, int Day)
        {
            List<Times.HourSubSlots> Slots = new List<Times.HourSubSlots>();
            for (int Hour = 0; Hour < length; Hour++)
                Slots.Add((Times.HourSubSlots)Enum.Parse(typeof(Times.HourSubSlots), Enum.GetName(typeof(Times.HourSubSlots), getSlot(programSpecialTimings, Day, Hour, order))));
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

                if (ID.HasValue && await programRepository.IsExists(ID.Value))
                {
                    program = await programRepository.GetById(ID.Value);
                    TimingList timingLists = new TimingList(24, 24, 24, 24, 24, 24, 24);
                    var programSpecialTimings = await programSpecialTimingRepository.GetByProgram(ID.Value);
                    timingLists.MondaySlots.Is_Hour_Slots = checkAllHours(1, timingLists.MondaySlots.Is_Hour_Slots, "Details", timingLists.MondaySlots.Hour_Start_Slots, timingLists.MondaySlots.Hour_End_Slots);
                    timingLists.MondaySlots.Hour_End_Slots = getAllSlotsValues(programSpecialTimings, false, timingLists.MondaySlots.Is_Hour_Slots.Count(), 1);
                    timingLists.MondaySlots.Hour_Start_Slots = getAllSlotsValues(programSpecialTimings, true, timingLists.MondaySlots.Is_Hour_Slots.Count(), 1);
                    timingLists.TuesdaySlots.Is_Hour_Slots = checkAllHours(2, timingLists.TuesdaySlots.Is_Hour_Slots, "Details", timingLists.TuesdaySlots.Hour_Start_Slots, timingLists.TuesdaySlots.Hour_End_Slots);
                    timingLists.TuesdaySlots.Hour_Start_Slots = getAllSlotsValues(programSpecialTimings, true, timingLists.TuesdaySlots.Is_Hour_Slots.Count(), 2);
                    timingLists.TuesdaySlots.Hour_End_Slots = getAllSlotsValues(programSpecialTimings, false, timingLists.TuesdaySlots.Is_Hour_Slots.Count(), 2);
                    timingLists.WednesdaySlots.Is_Hour_Slots = checkAllHours(3, timingLists.WednesdaySlots.Is_Hour_Slots, "Details", timingLists.WednesdaySlots.Hour_Start_Slots, timingLists.WednesdaySlots.Hour_End_Slots);
                    timingLists.WednesdaySlots.Hour_Start_Slots = getAllSlotsValues(programSpecialTimings, true, timingLists.WednesdaySlots.Is_Hour_Slots.Count(), 3);
                    timingLists.WednesdaySlots.Hour_End_Slots = getAllSlotsValues(programSpecialTimings, false, timingLists.WednesdaySlots.Is_Hour_Slots.Count(), 3);
                    timingLists.ThursdaySlots.Is_Hour_Slots = checkAllHours(4, timingLists.ThursdaySlots.Is_Hour_Slots, "Details", timingLists.ThursdaySlots.Hour_Start_Slots, timingLists.ThursdaySlots.Hour_End_Slots);
                    timingLists.ThursdaySlots.Hour_Start_Slots = getAllSlotsValues(programSpecialTimings, true, timingLists.ThursdaySlots.Is_Hour_Slots.Count(), 4);
                    timingLists.ThursdaySlots.Hour_End_Slots = getAllSlotsValues(programSpecialTimings, false, timingLists.ThursdaySlots.Is_Hour_Slots.Count(), 4);
                    timingLists.FridaySlots.Is_Hour_Slots = checkAllHours(5, timingLists.FridaySlots.Is_Hour_Slots, "Details", timingLists.FridaySlots.Hour_Start_Slots, timingLists.FridaySlots.Hour_End_Slots);
                    timingLists.FridaySlots.Hour_Start_Slots = getAllSlotsValues(programSpecialTimings, true, timingLists.FridaySlots.Is_Hour_Slots.Count(), 5);
                    timingLists.FridaySlots.Hour_End_Slots = getAllSlotsValues(programSpecialTimings, false, timingLists.FridaySlots.Is_Hour_Slots.Count(), 5);
                    timingLists.SaturdaySlots.Is_Hour_Slots = checkAllHours(6, timingLists.SaturdaySlots.Is_Hour_Slots, "Details", timingLists.SaturdaySlots.Hour_Start_Slots, timingLists.SaturdaySlots.Hour_End_Slots);
                    timingLists.SaturdaySlots.Hour_Start_Slots = getAllSlotsValues(programSpecialTimings, true, timingLists.SaturdaySlots.Is_Hour_Slots.Count(), 6);
                    timingLists.SaturdaySlots.Hour_End_Slots = getAllSlotsValues(programSpecialTimings, false, timingLists.SaturdaySlots.Is_Hour_Slots.Count(), 6);
                    timingLists.SundaySlots.Is_Hour_Slots = checkAllHours(7, timingLists.SundaySlots.Is_Hour_Slots, "Details", timingLists.SundaySlots.Hour_Start_Slots, timingLists.SundaySlots.Hour_End_Slots);
                    timingLists.SundaySlots.Hour_Start_Slots = getAllSlotsValues(programSpecialTimings, true, timingLists.SundaySlots.Is_Hour_Slots.Count(), 7);
                    timingLists.SundaySlots.Hour_End_Slots = getAllSlotsValues(programSpecialTimings, false, timingLists.SundaySlots.Is_Hour_Slots.Count(), 7);
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
                    await programSpecialTimingRepository.RemoveRange(program.ProgramID);
                    await programSpecialTimingRepository.SaveChangesAsync();
                    timingLists.MondaySlots.Is_Hour_Slots = checkAllHours(1, timingLists.MondaySlots.Is_Hour_Slots, "New", timingLists.MondaySlots.Hour_Start_Slots, timingLists.MondaySlots.Hour_End_Slots);
                    timingLists.TuesdaySlots.Is_Hour_Slots = checkAllHours(2, timingLists.TuesdaySlots.Is_Hour_Slots, "New", timingLists.TuesdaySlots.Hour_Start_Slots, timingLists.TuesdaySlots.Hour_End_Slots);
                    timingLists.WednesdaySlots.Is_Hour_Slots = checkAllHours(3, timingLists.WednesdaySlots.Is_Hour_Slots, "New", timingLists.WednesdaySlots.Hour_Start_Slots, timingLists.WednesdaySlots.Hour_End_Slots);
                    timingLists.ThursdaySlots.Is_Hour_Slots = checkAllHours(4, timingLists.ThursdaySlots.Is_Hour_Slots, "New", timingLists.ThursdaySlots.Hour_Start_Slots, timingLists.ThursdaySlots.Hour_End_Slots);
                    timingLists.FridaySlots.Is_Hour_Slots = checkAllHours(5, timingLists.FridaySlots.Is_Hour_Slots, "New", timingLists.FridaySlots.Hour_Start_Slots, timingLists.FridaySlots.Hour_End_Slots);
                    timingLists.SaturdaySlots.Is_Hour_Slots = checkAllHours(6, timingLists.SaturdaySlots.Is_Hour_Slots, "New", timingLists.SaturdaySlots.Hour_Start_Slots, timingLists.SaturdaySlots.Hour_End_Slots);
                    timingLists.SundaySlots.Is_Hour_Slots = checkAllHours(7, timingLists.SundaySlots.Is_Hour_Slots, "New", timingLists.SundaySlots.Hour_Start_Slots, timingLists.SundaySlots.Hour_End_Slots);
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
                    ViewBag.TotalTimings = programSpecialTimingRepository.getCount(ID.Value).ToString();
                    return PartialView(await programRepository.GetById(ID.Value));
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
        public async Task<IActionResult> Delete(Programs program)
        {
            var msgType = "";
            var msg = "";
            try
            {
                if (await (programSpecialTimingRepository.IsAnyProgramEntryExistAsync(program.ProgramID)))
                {
                    await programSpecialTimingRepository.RemoveRange(program.ProgramID);
                    await programSpecialTimingRepository.SaveChangesAsync();
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
        public JsonResult getPrograms(string institute, string department)
        {
            return Json(programRepository.GetForSelectList(string.IsNullOrEmpty(institute) ? (int?)null : Convert.ToInt32(institute), string.IsNullOrEmpty(department) ? (int?)null : Convert.ToInt32(department), true));
        }

    }
}