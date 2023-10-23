using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Timetable_DateSheet_Generator.Data.DbContext;
using Timetable_DateSheet_Generator.Data.Repositories.Account;
using Timetable_DateSheet_Generator.Data.Repositories.Building;
using Timetable_DateSheet_Generator.Data.Repositories.Department;
using Timetable_DateSheet_Generator.Data.Repositories.FacultyMember;
using Timetable_DateSheet_Generator.Data.Repositories.Institute;
using Timetable_DateSheet_Generator.Data.Repositories.OfferedCourse;
using Timetable_DateSheet_Generator.Data.Repositories.Program;
using Timetable_DateSheet_Generator.Data.Repositories.Room;
using Timetable_DateSheet_Generator.Data.Repositories.Semester;
using Timetable_DateSheet_Generator.Data.Repositories.Student;
using Timetable_DateSheet_Generator.Data.Repositories.TimeTable;
using Timetable_DateSheet_Generator.Models;
using Timetable_DateSheet_Generator.Models.ViewModels;

namespace Timetable_DateSheet_Generator.Controllers.Administration
{
    [Route("Administration/Dashboard/[action]")]
    [Authorize(Roles = "Administrator")]
    public class DashboardController : Controller
    {
        private readonly BuildingRepository buildingRepository;
        private readonly InstituteRepository instituteRepository;
        private readonly AccountRepository accountRepository;
        private readonly DepartmentRepository departmentRepository;
        private readonly FacultyMemberRepository facultyRepository;
        private readonly OfferedCourseRepository offeredCourseRepository;
        private readonly ProgramRepository programRepository;
        private readonly RoomRepository roomRepository;
        private readonly SemesterRepository semesterRepository;
        private readonly StudentRepository studentRepositoy;
        private readonly TimeTableRepository timeTableRepository;
        private readonly IHostingEnvironment hostingEnvironment;
        public DashboardController(Timetable_DateSheet_Context timetable_DateSheet_Context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IHostingEnvironment _hostingEnvironment)
        {
            hostingEnvironment = _hostingEnvironment;
            buildingRepository = new BuildingRepository(timetable_DateSheet_Context);
            instituteRepository = new InstituteRepository(timetable_DateSheet_Context);
            accountRepository = new AccountRepository(timetable_DateSheet_Context, userManager, signInManager);
            departmentRepository = new DepartmentRepository(timetable_DateSheet_Context);
            facultyRepository = new FacultyMemberRepository(timetable_DateSheet_Context);
            offeredCourseRepository = new OfferedCourseRepository(timetable_DateSheet_Context);
            programRepository = new ProgramRepository(timetable_DateSheet_Context);
            roomRepository = new RoomRepository(timetable_DateSheet_Context);
            semesterRepository = new SemesterRepository(timetable_DateSheet_Context);
            studentRepositoy = new StudentRepository(timetable_DateSheet_Context);
            timeTableRepository=new TimeTableRepository(timetable_DateSheet_Context);
        }
        [HttpGet]
        public async Task<IActionResult> view(string w_Form, string _Action, string Message, string MessageType)
        {
            try
            {
                if (!string.IsNullOrEmpty(_Action) && _Action.Trim().ToLower().Contains("true")
                   && !string.IsNullOrEmpty(w_Form))
                {
                    if (w_Form.Trim().ToLower().Contains("activity"))
                        ViewBag.Action = "activity";
                    else if (w_Form.Trim().ToLower().Contains("pass"))
                        ViewBag.Action = "pass";
                    else if (w_Form.Trim().ToLower().Contains("account"))
                        ViewBag.Action = "account";
                    else if (w_Form.Trim().ToLower().Contains("pic"))
                        ViewBag.Action = "pic";
                    else;
                }

                if (!string.IsNullOrEmpty(Message))
                {
                    ViewBag.MessageType = MessageType;
                    ViewBag.Message = Message;
                }

                var user = accountRepository.GetUser(User.Identity.Name);
                double percentage = 0;
                DashboardViewModel dashboardViewModel = new DashboardViewModel
                {
                    profileView = user,
                };

                var institutes = await instituteRepository.GetAll(null);
                dashboardViewModel.totalInstitutes = institutes.Count;
                dashboardViewModel.institutesCreatedBy = institutes.Where(c => c.User == user.Id).Count();
                if (dashboardViewModel.totalInstitutes > 0)
                {
                    percentage = ((double)dashboardViewModel.institutesCreatedBy / (double)dashboardViewModel.totalInstitutes) * 100;
                    ViewBag.institutePer = Math.Round(percentage, 1).ToString() + "%";
                }
                else
                    ViewBag.institutePer = "0%";

                var buildings = await buildingRepository.GetAll(null);
                dashboardViewModel.totalBuilings = buildings.Count;
                dashboardViewModel.buildingsCreatedBy = buildings.Where(c => c.User == user.Id).Count();
                if (dashboardViewModel.totalBuilings > 0)
                {
                    percentage = ((double)dashboardViewModel.buildingsCreatedBy / (double)dashboardViewModel.totalBuilings) * 100;
                    ViewBag.buildingPer = Math.Round(percentage, 1).ToString() + "%";
                }
                else
                    ViewBag.buildingPer = "0%";

                var rooms = await roomRepository.GetAll(null);
                dashboardViewModel.totalRooms = rooms.Count;
                dashboardViewModel.roomsCreatedBy = rooms.Where(c => c.User == user.Id).Count();
                if (dashboardViewModel.totalRooms > 0)
                {
                    percentage = ((double)dashboardViewModel.roomsCreatedBy / (double)dashboardViewModel.totalRooms) * 100;
                    ViewBag.roomPer = Math.Round(percentage, 1).ToString() + "%";
                }
                else
                    ViewBag.roomPer = "0%";

                var departments = await departmentRepository.GetAll(null);
                dashboardViewModel.totalDepartments = departments.Count;
                dashboardViewModel.departmentsCreatedBy = departments.Where(c => c.User == user.Id).Count();
                if (dashboardViewModel.totalDepartments > 0)
                {
                    percentage = ((double)dashboardViewModel.departmentsCreatedBy / (double)dashboardViewModel.totalDepartments) * 100;
                    ViewBag.departmentPer = Math.Round(percentage, 1).ToString() + "%";
                }
                else
                    ViewBag.departmentPer = "0%";
                var programs = await programRepository.GetAll(null);
                dashboardViewModel.totalPrograms = programs.Count;
                dashboardViewModel.programsCreatedBy = programs.Where(c => c.User == user.Id).Count();
                if (dashboardViewModel.totalPrograms > 0)
                {
                    percentage = ((double)dashboardViewModel.programsCreatedBy / (double)dashboardViewModel.totalPrograms) * 100;
                    ViewBag.programPer = Math.Round(percentage, 1).ToString() + "%";
                }
                else
                    ViewBag.programPer = "0%";

                var semesters = await semesterRepository.GetAll(null);
                dashboardViewModel.totalSemesters = semesters.Count;
                dashboardViewModel.semestersCreatedBy = semesters.Where(c => c.User == user.Id).Count();
                if (dashboardViewModel.totalSemesters > 0)
                {
                    percentage = ((double)dashboardViewModel.semestersCreatedBy / (double)dashboardViewModel.totalSemesters) * 100;
                    ViewBag.semesterPer = Math.Round(percentage, 1).ToString() + "%";
                }
                else
                    ViewBag.semesterPer = "0%";

                var faculties = await facultyRepository.GetAll(null);
                dashboardViewModel.totalFaculties = faculties.Count;
                dashboardViewModel.facultiesCreatedBy = faculties.Where(c => c.User == user.Id).Count();
                if (dashboardViewModel.totalFaculties > 0)
                {
                    percentage = ((double)dashboardViewModel.facultiesCreatedBy / (double)dashboardViewModel.totalFaculties) * 100;
                    ViewBag.facultyPer = Math.Round(percentage, 1).ToString() + "%";
                }
                else
                    ViewBag.facultyPer = "0%";

                var students = await studentRepositoy.GetAll(null);
                dashboardViewModel.totalStudents = students.Count;
                dashboardViewModel.studentsCreatedBy = students.Where(c => c.User == user.Id).Count();
                if (dashboardViewModel.totalStudents > 0)
                {
                    percentage = ((double)dashboardViewModel.studentsCreatedBy / (double)dashboardViewModel.totalStudents) * 100;
                    ViewBag.studentPer = Math.Round(percentage, 1).ToString() + "%";
                }
                else
                    ViewBag.studentPer = "0%";
                var courses = await offeredCourseRepository.GetAll(null);
                dashboardViewModel.totalOfferedCourses = courses.Count;
                dashboardViewModel.offeredCoursesCreatedBy = courses.Where(c => c.User == user.Id).Count();
                if (dashboardViewModel.totalOfferedCourses > 0)
                {
                    percentage = ((double)dashboardViewModel.offeredCoursesCreatedBy / (double)dashboardViewModel.totalOfferedCourses) * 100;
                    ViewBag.coursePer = Math.Round(percentage, 1).ToString() + "%";
                }
                else
                    ViewBag.coursePer = "0%";

                var timetables = await timeTableRepository.GetAll(null);
                dashboardViewModel.totalTimetables = timetables.Count;
                dashboardViewModel.timeTablesCreatedBy = timetables.Where(c => c.User == user.Id).Count();
                if (dashboardViewModel.totalTimetables > 0)
                {
                    percentage = ((double)dashboardViewModel.timeTablesCreatedBy / (double)dashboardViewModel.totalTimetables) * 100;
                    ViewBag.TimetablePer = Math.Round(percentage, 1).ToString() + "%";
                }
                else
                    ViewBag.TimetablePer = "0%";
                return View(dashboardViewModel);
            }
            catch
            {
                ViewBag.MessageType = Common.Error;
                ViewBag.Message = Common.NotFound;
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(DashboardViewModel dashboardViewModel)
        {
            var msg = "";
            var msgType = "";
            var errors = "";
            var tempID = "";
            try
            {
                if (string.IsNullOrEmpty(dashboardViewModel.Password) || string.IsNullOrEmpty(dashboardViewModel.ConfirmPassword))
                {
                    errors += "Password Field is Requied.";
                    return RedirectToAction("View", new { w_Form = "pass", _Action = "true", MessageType = Common.Error, Message = errors });
                }
                var temp = accountRepository.GetUserByID(dashboardViewModel.profileView.Id);
                tempID = temp.Id;
                if (ModelState.IsValid)
                {
                    var passResult = await accountRepository.ValidatePassword(temp, dashboardViewModel.Password);
                    if (passResult.Succeeded)
                    {
                        temp.PasswordHash = accountRepository.GeneratePasswordHash(temp, dashboardViewModel.Password);
                        var result = await accountRepository.UpdateAsync(temp);
                        if (result.Succeeded)
                        {
                            accountRepository.SignOutAsync();
                            var returnUrl = "~/Administration/Dashboard/View?w_Form='pass'&_Action='true'&MessageType='success'&Message='Password changed successfully.'";
                            return RedirectToAction("Login", "Account", new { ReturnUrl = returnUrl });
                        }
                        else
                        {
                            foreach (var Error in result.Errors)
                                errors += Error.Description + " \n";
                        }
                    }
                    else
                    {
                        foreach (var Error in passResult.Errors)
                            errors += Error.Description + " \n";
                    }
                }
                else
                {

                    foreach (var values in ModelState.Values)
                        foreach (var error in values.Errors)
                            errors += error.ErrorMessage + "\n";
                }
            }
            catch (Exception ex)
            {
                msgType = Common.Error;
                msg = Common.UpdateFail;
            }
            if (!string.IsNullOrEmpty(errors))
                return RedirectToAction("View", new { w_Form = "pass", _Action = "true", MessageType = Common.Error, Message = errors });
            return RedirectToAction("View", new { w_Form = "pass", _Action = "true", MessageType = msgType, Message = msg });

        }

        [HttpPost]
        public async Task<IActionResult> uploadPic(DashboardViewModel dashboardViewModel)
        {
            var msg = "";
            var msgType = "";
            var errors = "";
            var imagePath = "";
            var folderPath = "";
            try
            {
                if (ModelState.IsValid)
                {
                    if (dashboardViewModel.image != null)
                    {
                        var user = accountRepository.GetUserByID(dashboardViewModel.profileView.Id);
                        folderPath = "Users/" + Guid.NewGuid().ToString() + dashboardViewModel.image.FileName;
                        imagePath = Path.Combine(hostingEnvironment.WebRootPath, folderPath);
                        folderPath = "/" + folderPath;
                        if (!Directory.Exists(Path.Combine(hostingEnvironment.WebRootPath, "Users/")))
                            Directory.CreateDirectory(imagePath);
                        await dashboardViewModel.image.CopyToAsync(new FileStream(imagePath, FileMode.Create));
                        user.Image = string.IsNullOrEmpty(folderPath) ? null : folderPath;
                        await accountRepository.UpdateAsync(user);
                        msgType = Common.Success;
                        msg = "Profile Picture Uploaded Successfully.";
                    }
                }
                else
                {
                    foreach (var values in ModelState.Values)
                        foreach (var error in values.Errors)
                            errors += error.ErrorMessage + "\n";
                }
            }
            catch (Exception ex)
            {
                msgType = Common.Error;
                msg = Common.InsertFail;
            }
            if (!string.IsNullOrEmpty(errors))
                return RedirectToAction("View", new { w_Form = "pic", _Action = "true", MessageType = Common.Error, Message = errors });
            return RedirectToAction("View", new { w_Form = "pic", _Action = "true", MessageType = msgType, Message = msg });
        }
        [HttpPost]
        public async Task<IActionResult> UpdateAccount(DashboardViewModel dashboardViewModel)
        {
            var msgType = "";
            var msg = "";
            var errors = "";
            try
            {
                if (ModelState.IsValid)
                {
                    if (await accountRepository.isExists(dashboardViewModel.profileView.Id))
                    {

                        var user = accountRepository.GetUserByID(dashboardViewModel.profileView.Id);
                        user.Email = dashboardViewModel.profileView.Email;
                        user.Name = dashboardViewModel.profileView.Name;
                        user.Address = dashboardViewModel.profileView.Address;
                        user.Experience = dashboardViewModel.profileView.Experience;
                        user.Skills = dashboardViewModel.profileView.Skills;
                        await accountRepository.UpdateAsync(user);
                        accountRepository.SaveChangesAsync();
                        msgType = Common.Success;
                        msg = Common.UpdateSuccess;
                    }
                    else
                    {
                        msgType = Common.Error;
                        msg = Common.NotFound;
                    }
                }
                else
                {
                    foreach (var values in ModelState.Values)
                        foreach (var error in values.Errors)
                            errors += error.ErrorMessage + "\n";
                }
            }
            catch (Exception ex)
            {
                msgType = Common.Error;
                msg = Common.Fail;
            }
            if (!string.IsNullOrEmpty(errors))
                return RedirectToAction("View", new { w_Form = "account", _Action = "true", MessageType = Common.Error, Message = errors });
            return RedirectToAction("View", new { w_Form = "account", _Action = "true", MessageType = msgType, Message = msg });
        }
    }
}