using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Timetable_DateSheet_Generator.Data.DbContext;
using Timetable_DateSheet_Generator.Data.Repositories.Account;
using Timetable_DateSheet_Generator.Data.Repositories.Attendances;
using Timetable_DateSheet_Generator.Data.Repositories.FacultyMember;
using Timetable_DateSheet_Generator.Data.Repositories.OfferedCourse;
using Timetable_DateSheet_Generator.Data.Repositories.RegisteredCourse;
using Timetable_DateSheet_Generator.Data.Repositories.Student;
using Timetable_DateSheet_Generator.Data.Repositories.StudentAttendances;
using Timetable_DateSheet_Generator.Models;
using Timetable_DateSheet_Generator.Models.ViewModels;

namespace Timetable_DateSheet_Generator.Controllers.Student
{
    [Route("Student/Courses/[action]")]
    [Authorize(Roles = "Student")]
    public class StudentCoursesController : Controller
    {
        private readonly AccountRepository accountRepository;
        private readonly OfferedCourseRepository offeredCourseRepository;
        private readonly FacultyMemberRepository facultyMemberRepository;
        private readonly AttendanceRepository attendanceRepository;
        private readonly StudentAttendanceRepository studentAttendanceRepository;
        private readonly RegisteredCourseRepository registeredCourseRepository;
        private readonly StudentRepository studentRepository;
        public StudentCoursesController(Timetable_DateSheet_Context timetable_DateSheet_Context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            offeredCourseRepository = new OfferedCourseRepository(timetable_DateSheet_Context);
            facultyMemberRepository = new FacultyMemberRepository(timetable_DateSheet_Context);
            accountRepository = new AccountRepository(timetable_DateSheet_Context, userManager, signInManager);
            attendanceRepository = new AttendanceRepository(timetable_DateSheet_Context);
            studentAttendanceRepository = new StudentAttendanceRepository(timetable_DateSheet_Context);
            registeredCourseRepository = new RegisteredCourseRepository(timetable_DateSheet_Context);
            studentRepository = new StudentRepository(timetable_DateSheet_Context);
        }
        public async Task<IActionResult> ViewCourses()
        {
            var list = new List<RegisteredCourses>();
            try
            {
                var user = await accountRepository.GetUserAsync(User.Identity.Name);
                var student = await studentRepository.GetByUserId(user.Id);
                list = await registeredCourseRepository.GetByStudent(student.StudentID);
            }
            catch { }
            return View(list);
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
                    var obj = await offeredCourseRepository.GetById(ID.Value);
                    if (!string.IsNullOrEmpty(obj.FacultyMember.UserID))
                    {
                        var fa = await accountRepository.GetUserByIDAsync(obj.FacultyMember.UserID);
                        if (fa != null)
                            ViewBag.fName = fa.Name;
                        else
                            ViewBag.fName = "";
                    }
                    else
                        ViewBag.fName = "";
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
            return RedirectToAction("ViewCourses");
        }
        [HttpGet]
        public async Task<IActionResult> AttendanceSummary(int? Course)
        {
            var list = new List<StudentAttendanceViewModel>();
            try
            {
                var regCourse = await registeredCourseRepository.GetById(Course.Value);
                ViewBag.Course = regCourse.OfferedCourse;                
                double totalHours = 0;
                double totalPresentHours = 0;
                double totalAbsentHours = 0;
                string percentage = "0 %";
                string AbsentPercentage = "0 %";
                string flag = "n";
                foreach (var attendance in await attendanceRepository.GetByCourse(regCourse.OfferedCourseID))
                {
                    totalHours += attendance.AttendanceCreditHours;
                    var temp = new StudentAttendanceViewModel
                    {
                        Attendance = attendance,
                        Student = regCourse.Student
                    };
                    if (await studentAttendanceRepository.IsExists(regCourse.RegisteredCourseID, attendance.AttendanceID))
                    {
                        var Obj = await studentAttendanceRepository.GetByAttendance_RegId(attendance.AttendanceID, regCourse.RegisteredCourseID);
                        temp.isPresent = Obj.IsPresent;
                        if (Obj.IsPresent)
                            temp.PresentHours = attendance.AttendanceCreditHours;
                        else
                            temp.AbsentHours = attendance.AttendanceCreditHours;
                    }
                    else
                        temp.AbsentHours = attendance.AttendanceCreditHours;
                    totalPresentHours += temp.PresentHours;
                    totalAbsentHours += temp.AbsentHours;
                    list.Add(temp);
                }
                if (totalHours > 0)
                {
                    if ((totalPresentHours / totalHours) * 100 >= 75)
                        flag = "y";
                    percentage = ((totalPresentHours / totalHours) * 100).ToString() + " %";
                    AbsentPercentage = ((totalAbsentHours / totalHours) * 100).ToString() + " %";
                }
                ViewBag.TotalHours = totalHours;
                ViewBag.TotalPresentHours = totalPresentHours;
                ViewBag.TotalAbsentHours = totalAbsentHours;
                ViewBag.Percentage = percentage;
                ViewBag.AbsentPercentage = AbsentPercentage;
                ViewBag.Flag = flag;

            }
            catch { }
            return PartialView(list);
        }
    }
}