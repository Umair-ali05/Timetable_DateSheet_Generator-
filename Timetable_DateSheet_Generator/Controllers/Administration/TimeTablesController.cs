using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timetable_DateSheet_Generator.Data.Algorithms.GeneticAlgorithmForTimetable;
using Timetable_DateSheet_Generator.Data.DbContext;
using Timetable_DateSheet_Generator.Data.Repositories.Account;
using Timetable_DateSheet_Generator.Data.Repositories.Algorithm;
using Timetable_DateSheet_Generator.Data.Repositories.Algorithm.Report;
using Timetable_DateSheet_Generator.Data.Repositories.Building;
using Timetable_DateSheet_Generator.Data.Repositories.Department;
using Timetable_DateSheet_Generator.Data.Repositories.Institute;
using Timetable_DateSheet_Generator.Data.Repositories.OfferedCourse;
using Timetable_DateSheet_Generator.Data.Repositories.OfferedCourseTimeSlot;
using Timetable_DateSheet_Generator.Data.Repositories.Program;
using Timetable_DateSheet_Generator.Data.Repositories.ProgramRegularTiming;
using Timetable_DateSheet_Generator.Data.Repositories.ProgramSpecialTiming;
using Timetable_DateSheet_Generator.Data.Repositories.Room;
using Timetable_DateSheet_Generator.Data.Repositories.RoomAvailibility;
using Timetable_DateSheet_Generator.Data.Repositories.Semester;
using Timetable_DateSheet_Generator.Data.Repositories.TimeTable;
using Timetable_DateSheet_Generator.Models;
using Timetable_DateSheet_Generator.Models.ViewModels;

namespace Timetable_DateSheet_Generator.Controllers.Administration
{
    public struct Summary
    {
        public Programs Program { get; set; }
        public int numberOfTheoryCourses { get; set; }
        public int numberOfLabCourse { get; set; }
        public int numberOfClassrooms { get; set; }
        public int numberOfLabs { get; set; }
    }
    public struct Report
    {
        public List<OfferedCourseTimeSlots> OfferedCourseTimeSlots { get; set; }
        public bool ClassClash { get; set; }
        public bool RoomClash { get; set; }
        public bool FacultyClash { get; set; }
        public bool RoomTimings { get; set; }
        public bool ProgamTimings { get; set; }
        public string FacultyAvailibilty { get; set; }
    }
    [Route("Administration/TimeTables/[action]")]
    [Authorize(Roles = "Administrator")]
    public class TimeTablesController : Controller
    {
        private readonly TimeTableRepository timeTableRepository;
        private readonly InstituteRepository instituteRepository;
        private readonly BuildingRepository buildingRepository;
        private readonly RoomRepository roomRepository;
        private readonly SemesterRepository semesterRepository;
        private readonly AccountRepository accountRepository;
        private readonly ProgramRepository programRepository;
        private readonly OfferedCourseRepository offeredCourseRepository;
        private readonly RoomAvailibilityRepository roomAvailibilityRepository;
        private readonly ProgramRegularTimingRepository programRegularTimingRepository;
        private readonly ProgramSpecialTimingRepository programSpecialTimingRepository;
        private readonly CourseTimeSlotRepository courseTimeSlotRepository;
        private readonly AlgorithmTimeTableRepository _AlgorithmRepository;
        private readonly ReportRepository _report_Service;
        private readonly DepartmentRepository departmentRepository;

        public static Chromosome FittestChromosome;

        private static TimeTables TimeTable = new TimeTables();
        private readonly Random _random = new Random();

        public TimeTablesController(Timetable_DateSheet_Context timetable_DateSheet_Context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            accountRepository = new AccountRepository(timetable_DateSheet_Context, userManager, signInManager);
            timeTableRepository = new TimeTableRepository(timetable_DateSheet_Context);
            instituteRepository = new InstituteRepository(timetable_DateSheet_Context);
            semesterRepository = new SemesterRepository(timetable_DateSheet_Context);
            programRepository = new ProgramRepository(timetable_DateSheet_Context);
            offeredCourseRepository = new OfferedCourseRepository(timetable_DateSheet_Context);
            roomAvailibilityRepository = new RoomAvailibilityRepository(timetable_DateSheet_Context);
            roomRepository = new RoomRepository(timetable_DateSheet_Context);
            buildingRepository = new BuildingRepository(timetable_DateSheet_Context);
            departmentRepository = new DepartmentRepository(timetable_DateSheet_Context);
            courseTimeSlotRepository = new CourseTimeSlotRepository(timetable_DateSheet_Context);
            programRegularTimingRepository = new ProgramRegularTimingRepository(timetable_DateSheet_Context);
            programSpecialTimingRepository = new ProgramSpecialTimingRepository(timetable_DateSheet_Context);
            _AlgorithmRepository = new AlgorithmTimeTableRepository(timetable_DateSheet_Context);
            _report_Service = new ReportRepository(timetable_DateSheet_Context);
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
                return View(await timeTableRepository.GetAll(null));
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
            return PartialView();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("TimeTableID,InstituteID,SemesterID")] TimeTables TimeTable)
        {
            var msg = "";
            var msgType = "";
            try
            {
                if (ModelState.IsValid)
                {
                    if (!await timeTableRepository.IsExists(TimeTable.InstituteID, TimeTable.SemesterID, null))
                    {
                        var user = accountRepository.GetUser(User.Identity.Name);
                        if (user != null)
                            TimeTable.User = user.Id;
                        TimeTable.Last_Modified = DateTime.Now;
                        await timeTableRepository.Insert(TimeTable);
                        await timeTableRepository.SaveChangesAsync();
                        msgType = Common.Success;
                        msg = Common.InsertSuccess;
                    }
                    else
                    {
                        msg = Common.AlreadyExists;
                        msgType = Common.Error;
                    }
                }
                else
                {

                    var result = "";
                    foreach (var values in ModelState.Values)
                        foreach (var error in values.Errors)
                            result += error.ErrorMessage + "\n";
                    return RedirectToAction("Actions", new { w_Form = "create", _Action = "true", MessageType = Common.Error, Message = result });
                }
            }
            catch (Exception ex)
            {
                msgType = Common.Error;
                msg = Common.InsertFail;
            }
            return RedirectToAction("Actions", new { MessageType = msgType, Message = msg });
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
                    var Obj = await timeTableRepository.GetById(ID.Value);
                    ViewData["Institutes"] = new SelectList(instituteRepository.GetForSelectList(), "ID", "Name", Obj.InstituteID);
                    ViewData["Semesters"] = new SelectList(semesterRepository.GetForSelectList(), "ID", "Name", Obj.SemesterID);
                    return PartialView(Obj);
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
        public async Task<IActionResult> Edit([Bind("TimeTableID,InstituteID,SemesterID")] TimeTables TimeTable)
        {
            var msgType = "";
            var msg = "";
            try
            {
                if (ModelState.IsValid)
                {
                    if (await timeTableRepository.IsExists(TimeTable.TimeTableID))
                    {
                        if (!await timeTableRepository.IsExists(TimeTable.InstituteID, TimeTable.SemesterID, TimeTable.TimeTableID))
                        {
                            var user = accountRepository.GetUser(User.Identity.Name);
                            if (user != null)
                                TimeTable.User = user.Id;
                            TimeTable.Last_Modified = DateTime.Now;
                            timeTableRepository.Update(TimeTable);
                            await timeTableRepository.SaveChangesAsync();
                            msgType = Common.Success;
                            msg = Common.UpdateSuccess;
                        }
                        else
                        {
                            msgType = Common.Error;
                            msg = Common.AlreadyExists;
                        }
                    }
                    else
                    {
                        msgType = Common.Error;
                        msg = Common.NotFound;
                    }
                }
                else
                {
                    var result = "";
                    foreach (var values in ModelState.Values)
                        foreach (var error in values.Errors)
                            result += error.ErrorMessage + "\n";
                    return RedirectToAction("Actions", new { w_Form = "edit", _Action = "true", MessageType = Common.Error, Message = result, ID = TimeTable.TimeTableID });
                }
            }
            catch (Exception ex)
            {
                msgType = Common.Error;
                msg = Common.Fail;
            }
            return RedirectToAction("Actions", new { MessageType = msgType, Message = msg });
        }
        [HttpGet]
        public async Task<IActionResult> Details(int? ID)
        {
            var msgType = "";
            var msg = "";
            try
            {
                if (ID.HasValue)
                {
                    var obj = await timeTableRepository.GetById(ID.Value);
                    if (string.IsNullOrEmpty(obj.User))
                    {
                        ViewBag.Name = "";
                        ViewBag.Email = "";
                        ViewBag.Pic = "";
                    }
                    else
                    {
                        var user = await accountRepository.GetUserByIDAsync(obj.User);
                        ViewBag.Name = user.Name;
                        ViewBag.Email = user.Email;
                        ViewBag.Pic = user.Image;
                    }
                    return PartialView(obj);
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
        [HttpGet]
        public async Task<IActionResult> Delete(int? ID)
        {
            var msgType = "";
            var msg = "";
            try
            {
                if (ID.HasValue)
                    return PartialView(await timeTableRepository.GetById(ID.Value));
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
        public async Task<IActionResult> Delete([Bind("TimeTableID,InstituteID,SemesterID")] TimeTables TimeTable)
        {
            var msgType = "";
            var msg = "";
            try
            {
                if (await timeTableRepository.IsExists(TimeTable.TimeTableID))
                {
                    await timeTableRepository.Delete(TimeTable.TimeTableID);
                    await timeTableRepository.SaveChangesAsync();
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
        [HttpGet]
        public async Task<IActionResult> DataSummary(int? TimeTableID)
        {
            var msg = "";
            var msgType = "";
            try
            {
                if (TimeTableID.HasValue)
                {
                    if (await timeTableRepository.IsExists(TimeTableID.Value))
                    {
                        var timetable = await timeTableRepository.GetById(TimeTableID.Value);
                        List<Summary> _Summary = new List<Summary>();
                        var programs = await programRepository.GetProgramsByInstitute(timetable.InstituteID);
                        foreach (var program in programs)
                        {
                            Summary summary = new Summary
                            {
                                Program = program,
                                numberOfTheoryCourses = offeredCourseRepository.GetTheoryCoursesCount(program.ProgramID, timetable.InstituteID, timetable.SemesterID),
                                numberOfLabCourse = offeredCourseRepository.GetLabCoursesCount(program.ProgramID, timetable.InstituteID, timetable.SemesterID)
                            };
                            var temp = await roomAvailibilityRepository.GetGroupedRoomsCount(program.DepartmentID);
                            summary.numberOfClassrooms = temp.NumberOfClassRooms;
                            summary.numberOfLabs = temp.NumberOfLabs;
                            _Summary.Add(summary);
                        }
                        ViewBag.TimeTable = timetable;
                        return View(_Summary);
                    }
                }
                else
                {
                    msg = Common.NotFound;
                    msgType = Common.Error;
                }
            }
            catch
            {
                msg = Common.Fail;
                msgType = Common.Error;
            }
            return RedirectToAction("Actions", new { MessageType = msgType, Message = msg });
        }
        public async Task<IActionResult> GenerateTimetable(int? TimeTableID)
        {
            var msg = "";
            var msgType = "";
            List<Report> Report = new List<Report>();
            try
            {
                if (TimeTableID.HasValue)
                {
                    var timetable = await timeTableRepository.GetById(TimeTableID.Value);
                    ViewBag.TimeTable = timetable;
                    GeneticAlgorithm geneticAlgorithm = new GeneticAlgorithm(_AlgorithmRepository, timetable.InstituteID, timetable.SemesterID, timetable.TimeTableID);
                    await Task.Run(() => geneticAlgorithm.GenerateTimeTable());
                    FittestChromosome = geneticAlgorithm.getFittestChromosome();
                    ViewBag.Results = await Task.Run(() => geneticAlgorithm.getAllResults());
                    ViewBag.Best = await Task.Run(() => geneticAlgorithm.GetFittestResult());
                    ViewBag.MessageType = geneticAlgorithm.MessageType;
                    ViewBag.Message = geneticAlgorithm.Message;
                    if (FittestChromosome == null)
                        return PartialView(Report);
                    var Solution = FittestChromosome.getSolution();
                    if (Solution != null)
                    {
                        var CoursesScheduled = Solution.GroupBy(c => new { c.AssignedContactHours, c.OfferedCourseID, c.TimeSlots.Time.TimeWeekDay, c.CourseScheduleNumber }).ToList();
                        foreach (var Course in CoursesScheduled)
                        {
                            Report report = new Report
                            {
                                OfferedCourseTimeSlots = new List<OfferedCourseTimeSlots>()
                            };
                            report.OfferedCourseTimeSlots = Course.ToList();
                            report.ProgamTimings = await Task.Run(() => checkProgramTimings(Course.ToList()));
                            report.RoomTimings = await Task.Run(() => checkRoomTimings(Course.ToList()));
                            report.RoomClash = await Task.Run(() => checkRoomClash(Course.ToList()));
                            report.ClassClash = await Task.Run(() => checkClassClash(Course.ToList()));
                            report.FacultyClash = await Task.Run(() => checkFacultyClash(Course.ToList()));
                            report.FacultyAvailibilty = await Task.Run(() => getClassFitness(Course.ToList()).ToString() + "%");
                            Report.Add(report);
                        }
                    }
                }
                else
                {
                    msg = Common.NotFound;
                    msgType = Common.Error;
                }
            }
            catch
            {
                msg = Common.Fail;
                msgType = Common.Error;
            }
            return PartialView(Report);
        }
        public bool checkProgramTimings(List<OfferedCourseTimeSlots> offeredCourseTimeSlots)
        {
            if (offeredCourseTimeSlots.Count <= 0)
                return false;
            foreach (var CoursSlot in offeredCourseTimeSlots)
            {
                if (CoursSlot.OfferedCourse.OfferedCourseSpecial == 0)
                {
                    if (!_report_Service.CheckProgramRegularTimings(CoursSlot.TimeSlots.TimeID, CoursSlot.OfferedCourse.ProgramID))
                        return false;
                }
                else
                {
                    if (!_report_Service.CheckProgramSpecialTimings(CoursSlot.TimeSlots.TimeID, CoursSlot.OfferedCourse.ProgramID))
                        return false;
                }
            }
            return true;
        }
        public bool checkRoomTimings(List<OfferedCourseTimeSlots> offeredCourseTimeSlots)
        {
            if (offeredCourseTimeSlots.Count <= 0)
                return false;
            foreach (var CoursSlot in offeredCourseTimeSlots)
            {
                if (!_report_Service.CheckRoom(CoursSlot.TimeSlots.TimeID, CoursSlot.RoomID))
                    return false;
            }
            return true;
        }
        public bool checkRoomClash(List<OfferedCourseTimeSlots> offeredCourseTimeSlots)
        {
            if (offeredCourseTimeSlots.Count <= 0)
                return false;
            var solution = FittestChromosome.getSolution();
            foreach (var CoursSlot in offeredCourseTimeSlots)
            {
                if (solution.Where(c => c.RoomID == CoursSlot.RoomID && c.TimeSlots.TimeID == CoursSlot.TimeSlots.TimeID).ToList().Count > 1)
                    return false;
            }
            return true;
        }
        public bool checkFacultyClash(List<OfferedCourseTimeSlots> offeredCourseTimeSlots)
        {
            if (offeredCourseTimeSlots.Count <= 0)
                return false;
            var solution = FittestChromosome.getSolution();
            foreach (var CoursSlot in offeredCourseTimeSlots)
            {
                if (solution.Where(c => c.TimeSlots.TimeID == CoursSlot.TimeSlots.TimeID && c.OfferedCourse.FacultyMemberID == CoursSlot.OfferedCourse.FacultyMemberID).ToList().Count > 1)
                    return false;
            }
            return true;
        }
        public bool checkClassClash(List<OfferedCourseTimeSlots> offeredCourseTimeSlots)
        {
            if (offeredCourseTimeSlots.Count <= 0)
                return false;
            var solution = FittestChromosome.getSolution();
            var Class = solution.Where(c => c.OfferedCourseID != offeredCourseTimeSlots.ElementAt(0).OfferedCourseID && c.OfferedCourse.ProgramID == offeredCourseTimeSlots.ElementAt(0).OfferedCourse.ProgramID && c.OfferedCourse.OfferedCourseSemesterNo == offeredCourseTimeSlots.ElementAt(0).OfferedCourse.OfferedCourseSemesterNo && c.OfferedCourse.OfferedCourseSection == offeredCourseTimeSlots.ElementAt(0).OfferedCourse.OfferedCourseSection).ToList();
            foreach (var CoursSlot in offeredCourseTimeSlots)
            {
                if (Class.Any(c => c.TimeSlots.TimeID == CoursSlot.TimeSlots.TimeID))
                    return false;
            }
            return true;
        }
        public double getClassFitness(List<OfferedCourseTimeSlots> offeredCourseTimeSlots)
        {
            double fitness = 0;
            foreach (var courseSlot in offeredCourseTimeSlots)
            {
                if (!_report_Service.CheckFaculty(courseSlot.TimeSlots.TimeID, courseSlot.OfferedCourse.FacultyMemberID))
                    fitness += 1;
            }
            fitness = (fitness / offeredCourseTimeSlots.Count) * 100;
            return fitness;
        }
        public async Task<IActionResult> GenerateTimetableReport(int? Timetable)
        {
            var msg = "";
            var msgType = "";
            List<Report> Report = new List<Report>();
            try
            {
                if (Timetable.HasValue)
                {
                    var timetable = await timeTableRepository.GetById(Timetable.Value);
                    ViewBag.TimeTable = timetable;
                    if (FittestChromosome == null)
                    {
                        msgType = Common.Error;
                        msg = "Report not generated.";
                        ViewBag.Message = msg;
                        ViewBag.MessageType = msgType;
                        return PartialView(Report);
                    }
                    var Solution = FittestChromosome.getSolution();
                    if (Solution != null)
                    {
                        var CoursesScheduled = Solution.GroupBy(c => new { c.AssignedContactHours, c.OfferedCourseID, c.TimeSlots.Time.TimeWeekDay, c.CourseScheduleNumber }).ToList();
                        foreach (var Course in CoursesScheduled)
                        {
                            Report report = new Report
                            {
                                OfferedCourseTimeSlots = new List<OfferedCourseTimeSlots>()
                            };
                            report.OfferedCourseTimeSlots = Course.ToList();
                            report.ProgamTimings = await Task.Run(() => checkProgramTimings(Course.ToList()));
                            report.RoomTimings = await Task.Run(() => checkRoomTimings(Course.ToList()));
                            report.RoomClash = await Task.Run(() => checkRoomClash(Course.ToList()));
                            report.ClassClash = await Task.Run(() => checkClassClash(Course.ToList()));
                            report.FacultyClash = await Task.Run(() => checkFacultyClash(Course.ToList()));
                            report.FacultyAvailibilty = await Task.Run(() => getClassFitness(Course.ToList()).ToString() + "%");
                            Report.Add(report);
                        }
                        msgType = Common.Success;
                        msg = "Report generated Successfully.";
                    }
                    else
                    {
                        ViewBag.MessageType = Common.Error;
                        ViewBag.Message = "Report not generated.";
                    }
                }
                else
                {
                    msgType = Common.Error;
                    msg = "Report Not generated.";
                }

            }
            catch
            {
                msgType = Common.Error;
                msg = "Report Not generated.";
            }
            ViewBag.Message = msg;
            ViewBag.MessageType = msgType;
            return PartialView(Report);
        }

        public void GenerateColor(OfferedCourses offeredCourse)
        {
            if (Common.Colors.Count >= 34)
                return;
            else
            {
                int Color = _random.Next(0, 34);
                if (Common.Colors.Any(c => c.CourseID == offeredCourse.OfferedCourseID && c.Color == Color))
                    return;
                else if (Common.Colors.Any(c => c.Color == Color))
                    GenerateColor(offeredCourse);
                else
                {
                    Colors color = new Colors
                    {
                        Color = Color,
                        CourseID = offeredCourse.OfferedCourseID
                    };
                    Common.Colors.Add(color);
                }
            }
        }
        public async Task<IActionResult> DisplayTimetable(int? Timetable)
        {
            try
            {
                var timeTable = await timeTableRepository.GetById(Timetable.Value);
                TimeTable = timeTable;
                ViewData["Buildings"] = new SelectList(buildingRepository.GetForSelectList(timeTable.InstituteID), "ID", "Name");
                ViewData["Rooms"] = new SelectList(roomRepository.GetForSelectList_WithTimings(null, null), "ID", "Name");
                return View(timeTable);
            }
            catch { }
            return View(new TimeTables());
        }
        public async Task<IActionResult> ShowTimetable(int? room, int? timetable)
        {
            List<TimetableDisplay> DisplayList = new List<TimetableDisplay>();
            try
            {
                Common.Colors.Clear();
                var Room = await roomRepository.GetById(room.Value);
                var _timetable = await timeTableRepository.GetById(timetable.Value);
                ViewBag.Room = Room;
                TimetableDisplay timetableDisplay = new TimetableDisplay();
                var RoomSlots = roomAvailibilityRepository.getByRoom_Ordered(Room.RoomID);
                List<Times> Times = new List<Times>();
                for (int Hour = 0; Hour < 24; Hour++)
                {
                    foreach (var slot in (Times.HourSubSlots[])Enum.GetValues(typeof(Times.HourSubSlots)))
                    {
                        var Slot = slot.GetDisplayName();
                        Times times = new Times
                        {
                            TimeWeekDay = 1,
                            StartTime = Convert.ToInt32(Convert.ToString(Hour) + Slot.ToString()),
                            FinishTime = Convert.ToInt32(Convert.ToString(Hour) + (Convert.ToInt32(Slot) + 15).ToString()),
                            TimeID = Convert.ToInt32(Convert.ToString(1) + Convert.ToString(Hour) + Slot)
                        };
                        if (RoomSlots.Any(c => c.Time.StartTime == times.StartTime))
                        {
                            foreach (var _slot in (Times.HourSubSlots[])Enum.GetValues(typeof(Times.HourSubSlots)))
                            {
                                var _Slot = _slot.GetDisplayName();
                                Times _times = new Times
                                {
                                    TimeWeekDay = 1,
                                    StartTime = Convert.ToInt32(Convert.ToString(Hour) + _Slot.ToString()),
                                    FinishTime = Convert.ToInt32(Convert.ToString(Hour) + (Convert.ToInt32(_Slot) + 15).ToString()),
                                    TimeID = Convert.ToInt32(Convert.ToString(1) + Convert.ToString(Hour) + _Slot)
                                };
                                if (!Times.Any(c => c.StartTime == _times.StartTime))
                                    Times.Add(_times);
                            }
                        }
                    }
                }
                ViewBag.Times = Times;
                Times.OrderBy(c => c.TimeID);
                var MondayCourses = courseTimeSlotRepository.GetByRoom_Day_Timetable(Room.RoomID, 1, _timetable.TimeTableID);
                foreach (var MondayCourse in MondayCourses)
                {
                    GenerateColor(MondayCourse.ElementAt(0).OfferedCourse);
                    timetableDisplay.MondayCourseTimeSlots.Add(MondayCourse.ToList());
                }
                var TuesdayCourses = courseTimeSlotRepository.GetByRoom_Day_Timetable(Room.RoomID, 2, _timetable.TimeTableID); ;
                foreach (var TuesdayCourse in TuesdayCourses)
                {
                    GenerateColor(TuesdayCourse.ElementAt(0).OfferedCourse);
                    timetableDisplay.TuesdayCourseTimeSlots.Add(TuesdayCourse.ToList());
                }
                var WednesdayCourses = courseTimeSlotRepository.GetByRoom_Day_Timetable(Room.RoomID, 3, _timetable.TimeTableID);
                foreach (var WednesdayCourse in WednesdayCourses)
                {
                    GenerateColor(WednesdayCourse.ElementAt(0).OfferedCourse);
                    timetableDisplay.WednesdayCourseTimeSlots.Add(WednesdayCourse.ToList());
                }
                var ThursdayCourses = courseTimeSlotRepository.GetByRoom_Day_Timetable(Room.RoomID, 4, _timetable.TimeTableID);
                foreach (var ThursdayCourse in ThursdayCourses)
                {
                    GenerateColor(ThursdayCourse.ElementAt(0).OfferedCourse);
                    timetableDisplay.ThursdayCourseTimeSlots.Add(ThursdayCourse.ToList());
                }
                var FridayCourses = courseTimeSlotRepository.GetByRoom_Day_Timetable(Room.RoomID, 5, _timetable.TimeTableID);
                foreach (var FridayCourse in FridayCourses)
                {
                    GenerateColor(FridayCourse.ElementAt(0).OfferedCourse);
                    timetableDisplay.FridayCourseTimeSlots.Add(FridayCourse.ToList());
                }
                var SaturdayCourses = courseTimeSlotRepository.GetByRoom_Day_Timetable(Room.RoomID, 6, _timetable.TimeTableID);
                foreach (var SaturdayCourse in SaturdayCourses)
                {
                    GenerateColor(SaturdayCourse.ElementAt(0).OfferedCourse);
                    timetableDisplay.SaturdayCourseTimeSlots.Add(SaturdayCourse.ToList());
                }
                var SundayCourses = courseTimeSlotRepository.GetByRoom_Day_Timetable(Room.RoomID, 7, _timetable.TimeTableID);
                foreach (var SundayCourse in SundayCourses)
                {
                    GenerateColor(SundayCourse.ElementAt(0).OfferedCourse);
                    timetableDisplay.SundayCourseTimeSlots.Add(SundayCourse.ToList());
                }
                DisplayList.Add(timetableDisplay);
            }
            catch { }
            return PartialView(DisplayList);
        }

        public async Task<IActionResult> DisplayTimetableClassWise(int? timetable)
        {
            try
            {
                var timeTable = await timeTableRepository.GetById(timetable.Value);
                TimeTable = timeTable;
                ViewBag.TimeTable = timeTable;
                ViewData["Departments"] = new SelectList(departmentRepository.GetForSelectList(timeTable.InstituteID), "ID", "Name");
                ViewData["Programs"] = new SelectList(programRepository.GetForSelectList(TimeTable.InstituteID, null), "ID", "Name");
                return View(timeTable);
            }
            catch { }
            return View(new TimeTables());
        }
        public async Task<IActionResult> ShowTimetableClassWise(int? program, int? timetable)
        {
            try
            {
                Common.Colors.Clear();
                var _Program = await programRepository.GetById(program.Value);
                ViewBag.Program = _Program;
                var ProgramRegularSlots = await programRegularTimingRepository.GetOrdered_ByProgram(_Program.ProgramID);
                var ProgramSpecialSlots = await programSpecialTimingRepository.GetOrdered_ByProgram(_Program.ProgramID);
                List<Times> Times = new List<Times>();
                for (int Hour = 0; Hour < 24; Hour++)
                {
                    foreach (var slot in (Times.HourSubSlots[])Enum.GetValues(typeof(Times.HourSubSlots)))
                    {
                        var Slot = slot.GetDisplayName();
                        Times times = new Times
                        {
                            TimeWeekDay = 1,
                            StartTime = Convert.ToInt32(Convert.ToString(Hour) + Slot.ToString()),
                            FinishTime = Convert.ToInt32(Convert.ToString(Hour) + (Convert.ToInt32(Slot) + 15).ToString()),
                            TimeID = Convert.ToInt32(Convert.ToString(1) + Convert.ToString(Hour) + Slot)
                        };
                        if (ProgramRegularSlots.Any(c => c.Time.StartTime == times.StartTime) || ProgramSpecialSlots.Any(c => c.Time.StartTime == times.StartTime))
                        {
                            foreach (var _slot in (Times.HourSubSlots[])Enum.GetValues(typeof(Times.HourSubSlots)))
                            {
                                var _Slot = _slot.GetDisplayName();
                                Times _times = new Times
                                {
                                    TimeWeekDay = 1,
                                    StartTime = Convert.ToInt32(Convert.ToString(Hour) + _Slot.ToString()),
                                    FinishTime = Convert.ToInt32(Convert.ToString(Hour) + (Convert.ToInt32(_Slot) + 15).ToString()),
                                    TimeID = Convert.ToInt32(Convert.ToString(1) + Convert.ToString(Hour) + _Slot)
                                };
                                if (!Times.Any(c => c.StartTime == _times.StartTime))
                                    Times.Add(_times);
                            }
                        }
                    }
                }
                ViewBag.Times = Times;
                Times.OrderBy(c => c.TimeID);
                return PartialView(await courseTimeSlotRepository.GetByProgram_Timetable(_Program.ProgramID, timetable.Value));
            }
            catch { }
            return PartialView(new List<TimetableDisplay>());
        }


        public JsonResult getBuildings(string institute)
        {
            return Json(buildingRepository.GetForSelectList(string.IsNullOrEmpty(institute) ? (int?)null : Convert.ToInt32(institute)));
        }
        public JsonResult getRooms(string institute, string Building)
        {
            return Json(roomRepository.GetForSelectList_WithTimings(string.IsNullOrEmpty(institute) ? (int?)null : Convert.ToInt32(institute), string.IsNullOrEmpty(Building) ? (int?)null : Convert.ToInt32(Building)));
        }
        public JsonResult getPrograms(string institute, string department)
        {
            return Json(programRepository.GetForSelectList(string.IsNullOrEmpty(institute) ? (int?)null : Convert.ToInt32(institute), string.IsNullOrEmpty(department) ? (int?)null : Convert.ToInt32(department)));
        }
    }
}