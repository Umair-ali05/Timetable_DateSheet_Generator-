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
using Timetable_DateSheet_Generator.Data.Repositories.RegisteredCourse;
using Timetable_DateSheet_Generator.Data.Repositories.Room;
using Timetable_DateSheet_Generator.Data.Repositories.RoomAvailibility;
using Timetable_DateSheet_Generator.Data.Repositories.Semester;
using Timetable_DateSheet_Generator.Data.Repositories.Student;
using Timetable_DateSheet_Generator.Data.Repositories.TimeTable;
using Timetable_DateSheet_Generator.Models;
using Timetable_DateSheet_Generator.Models.ViewModels;

namespace Timetable_DateSheet_Generator.Controllers.Student
{
    [Route("Student/TimeTables/[action]")]
    [Authorize(Roles = "Student")]
    public class StudentTimeTablesController : Controller
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
        private readonly StudentRepository studentRepository;
        private readonly RegisteredCourseRepository registeredCourseRepository;

        public static Chromosome FittestChromosome;

        private static TimeTables TimeTable = new TimeTables();
        private readonly Random _random = new Random();

        public StudentTimeTablesController(Timetable_DateSheet_Context timetable_DateSheet_Context,
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
            registeredCourseRepository = new RegisteredCourseRepository(timetable_DateSheet_Context);
            studentRepository = new StudentRepository(timetable_DateSheet_Context);
            _AlgorithmRepository = new AlgorithmTimeTableRepository(timetable_DateSheet_Context);
            _report_Service = new ReportRepository(timetable_DateSheet_Context);
        }       
        public IActionResult DisplayTimetable()
        {
            try
            {
                ViewData["Semesters"] = new SelectList(semesterRepository.GetForSelectList(), "ID", "Name");
            }
            catch { }
            return View();
        }
       
        public async Task<IActionResult> ShowTimetable(int? semester)
        {
            try
            {
                if (semester.HasValue)
                {
                    var user = accountRepository.GetUser(User.Identity.Name);
                    var _student = await studentRepository.GetByUserId(user.Id);
                    var timetable = await timeTableRepository.GetByInstitute_Semester(_student.Program.Department.InstituteID, semester.Value);
                    TimeTable = timetable;
                    ViewBag.Timetable = TimeTable;
                    var StudentCourses = await registeredCourseRepository.GetByStudent_Semester(_student.StudentID, semester.Value);
                    var ProgramRegularSlots = await programRegularTimingRepository.GetOrdered_ByProgram(_student.ProgarmID);
                    var ProgramSpecialSlots = await programSpecialTimingRepository.GetOrdered_ByProgram(_student.ProgarmID);
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
                    return PartialView(await courseTimeSlotRepository.GetBy_Timetable(timetable.TimeTableID, StudentCourses));
                }
            }
            catch { }            
            return PartialView(new List<TimetableDisplay>());
        }
    }
}