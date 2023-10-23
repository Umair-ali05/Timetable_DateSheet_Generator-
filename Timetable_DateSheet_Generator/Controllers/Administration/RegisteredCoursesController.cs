using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Transactions;
using Timetable_DateSheet_Generator.Data.DbContext;
using Timetable_DateSheet_Generator.Data.Repositories.Account;
using Timetable_DateSheet_Generator.Data.Repositories.Department;
using Timetable_DateSheet_Generator.Data.Repositories.FacultyMember;
using Timetable_DateSheet_Generator.Data.Repositories.Institute;
using Timetable_DateSheet_Generator.Data.Repositories.OfferedCourse;
using Timetable_DateSheet_Generator.Data.Repositories.Program;
using Timetable_DateSheet_Generator.Data.Repositories.RegisteredCourse;
using Timetable_DateSheet_Generator.Data.Repositories.Semester;
using Timetable_DateSheet_Generator.Data.Repositories.Student;
using Timetable_DateSheet_Generator.Data.Repositories.StudentAttendances;
using Timetable_DateSheet_Generator.Models;
using Timetable_DateSheet_Generator.Models.ViewModels;

namespace Timetable_DateSheet_Generator.Controllers.Administration
{
    [Route("Administration/RegisteredCourses/[action]")]
    [Authorize(Roles = "Administrator")]
    public class RegisteredCoursesController : Controller
    {
        private readonly AccountRepository accountRepository;
        private readonly InstituteRepository instituteRepository;
        private readonly ProgramRepository programRepository;
        private readonly DepartmentRepository departmentRepository;
        private readonly SemesterRepository semesterRepository;
        private readonly OfferedCourseRepository offeredCourseRepository;
        private readonly FacultyMemberRepository facultyMemberRepository;
        private readonly RegisteredCourseRepository registeredCourseRepository;
        private readonly StudentRepository studentRepositoy;
        private readonly StudentAttendanceRepository studentAttendanceRepository;
        public RegisteredCoursesController(Timetable_DateSheet_Context timetable_DateSheet_Context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            facultyMemberRepository = new FacultyMemberRepository(timetable_DateSheet_Context);
            accountRepository = new AccountRepository(timetable_DateSheet_Context, userManager, signInManager);
            instituteRepository = new InstituteRepository(timetable_DateSheet_Context);
            departmentRepository = new DepartmentRepository(timetable_DateSheet_Context);
            programRepository = new ProgramRepository(timetable_DateSheet_Context);
            studentRepositoy = new StudentRepository(timetable_DateSheet_Context);
            semesterRepository = new SemesterRepository(timetable_DateSheet_Context);
            offeredCourseRepository = new OfferedCourseRepository(timetable_DateSheet_Context);
            registeredCourseRepository = new RegisteredCourseRepository(timetable_DateSheet_Context);
            studentAttendanceRepository = new StudentAttendanceRepository(timetable_DateSheet_Context);
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
                var list = await offeredCourseRepository.GetAll(null);
                List<string> Students = new List<string>();
                foreach (var item in list)
                {
                    var count = registeredCourseRepository.getCount(item.OfferedCourseID);
                    if (count > 0)
                        Students.Add(count.ToString());
                    else
                        Students.Add("No Student Registered.");
                }
                ViewData["StudentRegistered"] = Students;
                return View(list);
            }
            catch
            {
                ViewBag.MessageType = Common.Error;
                ViewBag.Message = Common.GetListFail;
                return View(new List<OfferedCourses>());
            }
        }
        [HttpGet]
        public IActionResult Create(string isNew)
        {
            if (!string.IsNullOrEmpty(isNew))
                ViewBag.Message = "Invalid Attempt";
            ViewData["Institutes"] = new SelectList(instituteRepository.GetForSelectList(), "ID", "Name");
            ViewData["Departments"] = new SelectList(departmentRepository.GetForSelectList(null), "ID", "Name");
            ViewData["Programs"] = new SelectList(programRepository.GetForSelectList(null), "ID", "Name");
            ViewData["OfferedCourses"] = new SelectList(offeredCourseRepository.GetForSelectList(null, null, null), "ID", "Name");
            return PartialView();
        }
        [HttpGet]
        public async Task<IActionResult> LoadStudents(int? Institute, int? offeredCourseID)
        {
            var Obj = new RegisterCourseViewModelList();
            try
            {
                if (offeredCourseID.HasValue)
                {
                    foreach (var student in await studentRepositoy.GetAllView(Institute))
                    {
                        if (!await registeredCourseRepository.IsExistsAsync(offeredCourseID.Value, student.Student.StudentID))
                            Obj.studentCourseDetailViewModel.Add(new StudentCourseDetailViewModel() { studentId = student.Student.StudentID, isMarked = false, programName = student.Student.Program.ProgramName, enrollment = student.Student.StudentEnrollment, User = student.Student.User, Name = student.RegisterViewModel.Name, Email = student.RegisterViewModel.UserEmail, path = student.RegisterViewModel.path, course = offeredCourseID.Value });
                    }
                    return PartialView(Obj);
                }
            }
            catch { }
            return PartialView(Obj);
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

                if (ID.HasValue && await offeredCourseRepository.IsExists(ID.Value))
                {
                    var obj = await offeredCourseRepository.GetById(ID.Value);
                    var ViewModel = new RegisterCourseViewModelList();
                    foreach (var student in await studentRepositoy.GetAllView(obj.Department.InstituteID))
                        ViewModel.studentCourseDetailViewModel.Add(new StudentCourseDetailViewModel() { studentId = student.Student.StudentID, isMarked = await registeredCourseRepository.IsExistsAsync(obj.OfferedCourseID, student.Student.StudentID), programName = student.Student.Program.ProgramName, enrollment = student.Student.StudentEnrollment, User = student.Student.User, Name = student.RegisterViewModel.Name, Email = student.RegisterViewModel.UserEmail, path = student.RegisterViewModel.path, course = obj.OfferedCourseID });
                    return PartialView(ViewModel);
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
        public async Task<IActionResult> Details(int? ID)
        {
            var msgType = "";
            var msg = "";
            try
            {
                if (ID.HasValue)
                {
                    var obj = await offeredCourseRepository.GetById(ID.Value);
                    if (obj != null)
                        ViewBag.Course = obj.OfferedCourseTitle;
                    var ViewModel = new RegisterCourseViewModelList();
                    foreach (var student in await studentRepositoy.GetAllView(obj.Department.InstituteID))
                        if (await registeredCourseRepository.IsExistsAsync(obj.OfferedCourseID, student.Student.StudentID))
                            ViewModel.studentCourseDetailViewModel
                                .Add(new StudentCourseDetailViewModel()
                                {
                                    studentId = student.Student.StudentID,
                                    isMarked = true,
                                    programName = student.Student.Program.ProgramName,
                                    enrollment = student.Student.StudentEnrollment,
                                    User = student.Student.User,
                                    Name = student.RegisterViewModel.Name,
                                    Email = student.RegisterViewModel.UserEmail,
                                    path = student.RegisterViewModel.path,
                                    course = obj.OfferedCourseID
                                });
                    return PartialView(ViewModel);
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
                {
                    ViewBag.TotalStudents = registeredCourseRepository.getCount(ID.Value).ToString();
                    return PartialView(await offeredCourseRepository.GetById(ID.Value));
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
        public async Task<IActionResult> Delete(OfferedCourses offeredCourse)
        {
            var msgType = "";
            var msg = "";
            try
            {
                if (await (registeredCourseRepository.IsAnyCourseEntryExist(offeredCourse.OfferedCourseID)))
                {
                    using (var ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        studentAttendanceRepository.RemoveByCourse(offeredCourse.OfferedCourseID);
                        studentAttendanceRepository.SaveChanges();
                        await registeredCourseRepository.RemoveRange(offeredCourse.OfferedCourseID);
                        await registeredCourseRepository.SaveChangesAsync();
                        msgType = Common.Success;
                        msg = Common.DeleteSuccess;
                        ts.Complete();
                    }
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
            return Json(programRepository.GetForSelectList(string.IsNullOrEmpty(institute) ? (int?)null : Convert.ToInt32(institute), string.IsNullOrEmpty(department) ? (int?)null : Convert.ToInt32(department)));
        }
        public void Add(string course, string student)
        {
            try
            {
                if (!string.IsNullOrEmpty(course) && !string.IsNullOrEmpty(student))
                {
                    if (!registeredCourseRepository.IsExists(Convert.ToInt32(course), Convert.ToInt32(student)))
                    {
                        registeredCourseRepository.InsertSync(new RegisteredCourses() { OfferedCourseID = Convert.ToInt32(course), StudentID = Convert.ToInt32(student) });
                        registeredCourseRepository.SaveChanges();
                    }
                }
            }
            catch { }
        }
        public void Remove(string course, string student)
        {
            try
            {
                if (!string.IsNullOrEmpty(course) && !string.IsNullOrEmpty(student))
                {
                    if (registeredCourseRepository.IsExists(Convert.ToInt32(course), Convert.ToInt32(student)))
                    {
                        using (var ts = new TransactionScope())
                        {
                            studentAttendanceRepository.RemoveByCourse_Student(Convert.ToInt32(course), Convert.ToInt32(student));
                            studentAttendanceRepository.SaveChanges();
                            registeredCourseRepository.RemoveByStudentAndCourse(Convert.ToInt32(student), Convert.ToInt32(course));
                            registeredCourseRepository.SaveChanges();
                            ts.Complete();
                        }
                    }
                }
            }
            catch { }
        }
        public JsonResult getCourses(string institute, string department, string program)
        {
            return Json(offeredCourseRepository.GetForSelectList(
                string.IsNullOrEmpty(institute) ? (int?)null : Convert.ToInt32(institute),
                string.IsNullOrEmpty(department) ? (int?)null : Convert.ToInt32(department),
                string.IsNullOrEmpty(program) ? (int?)null : Convert.ToInt32(program)));
        }
    }
}