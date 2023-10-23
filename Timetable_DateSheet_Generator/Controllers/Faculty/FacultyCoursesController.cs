using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Transactions;
using Timetable_DateSheet_Generator.Data.DbContext;
using Timetable_DateSheet_Generator.Data.Repositories.Account;
using Timetable_DateSheet_Generator.Data.Repositories.Attendances;
using Timetable_DateSheet_Generator.Data.Repositories.FacultyMember;
using Timetable_DateSheet_Generator.Data.Repositories.OfferedCourse;
using Timetable_DateSheet_Generator.Data.Repositories.RegisteredCourse;
using Timetable_DateSheet_Generator.Data.Repositories.StudentAttendances;
using Timetable_DateSheet_Generator.Models;
using Timetable_DateSheet_Generator.Models.ViewModels;

namespace Timetable_DateSheet_Generator.Controllers.Faculty
{
    [Route("Faculty/Courses/[action]")]
    [Authorize(Roles = "Faculty")]
    public class FacultyCoursesController : Controller
    {
        private readonly AccountRepository accountRepository;
        private readonly OfferedCourseRepository offeredCourseRepository;
        private readonly FacultyMemberRepository facultyMemberRepository;
        private readonly AttendanceRepository attendanceRepository;
        private readonly StudentAttendanceRepository studentAttendanceRepository;
        private readonly RegisteredCourseRepository registeredCourseRepository;
        public FacultyCoursesController(Timetable_DateSheet_Context timetable_DateSheet_Context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            offeredCourseRepository = new OfferedCourseRepository(timetable_DateSheet_Context);
            facultyMemberRepository = new FacultyMemberRepository(timetable_DateSheet_Context);
            accountRepository = new AccountRepository(timetable_DateSheet_Context, userManager, signInManager);
            attendanceRepository = new AttendanceRepository(timetable_DateSheet_Context);
            studentAttendanceRepository = new StudentAttendanceRepository(timetable_DateSheet_Context);
            registeredCourseRepository = new RegisteredCourseRepository(timetable_DateSheet_Context);
        }
        public async Task<IActionResult> Courses(string MessageType, string Message)
        {
            var msg = "";
            var msgType = "";
            var courses = new List<OfferedCourses>();
            try
            {

                var user = await accountRepository.GetUserAsync(User.Identity.Name);
                var faculty = await facultyMemberRepository.GetByUserId(user.Id);
                courses = await offeredCourseRepository.GetAll(faculty.FacultyMemberID);
                msg = Common.GetListSuccess;
                msgType = Common.Success;
            }
            catch
            {
                msg = Common.GetListFail;
                msgType = Common.Error;
            }
            if (string.IsNullOrEmpty(MessageType) || string.IsNullOrEmpty(Message))
            {
                ViewBag.MessageType = msgType;
                ViewBag.Message = msg;
            }
            else
            {
                ViewBag.MessageType = MessageType;
                ViewBag.Message = Message;
            }
            return View(courses);
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
            return RedirectToAction("Courses", new { MessageType = msgType, Message = msg });
        }
        public async Task<IActionResult> Attendance(int? Course, string w_Form, string _Action, string Message, string MessageType, int? ID)
        {
            try
            {
                if (!Course.HasValue)
                    return RedirectToAction("Courses", new { Message = Common.NotFound, MessageType = Common.Error });
                ViewBag.Course = Course;
                ViewBag.Obj = await offeredCourseRepository.GetById(Course.Value);
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
                return View(await attendanceRepository.GetByCourse(Course.Value));
            }
            catch
            {
                ViewBag.MessageType = Common.Error;
                ViewBag.Message = Common.GetListFail;
                return View(new List<Attendance>());
            }
        }
        [HttpGet]
        public async Task<IActionResult> Create(int? Course, string isNew)
        {
            var viewModel = new AttendanceViewModel();
            try
            {
                ViewBag.Course = await offeredCourseRepository.GetById(Course.Value);
                if (!string.IsNullOrEmpty(isNew))
                    ViewBag.Message = "Invalid Attempt";
                viewModel.Attendance = new Attendance() { OfferedCourseID = Course.Value };
                foreach (var regCourse in await registeredCourseRepository.GetByCourse(Course.Value))
                    viewModel.studentAttendances.Add(new StudentAttendance() { RegisteredCourse = regCourse });
            }
            catch { }
            return PartialView(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Attendance,studentAttendances")] AttendanceViewModel attendanceViewModel)
        {
            var msg = "";
            var msgType = "";
            try
            {
                if (ModelState.IsValid)
                {
                    using (var ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        attendanceViewModel.Attendance.AttendanceDate = DateTime.Now;
                        attendanceViewModel.Attendance.Last_Modified = DateTime.Now;
                        var user = accountRepository.GetUser(User.Identity.Name);
                        if (user != null)
                            attendanceViewModel.Attendance.User = user.Id;
                        attendanceRepository.InsertSync(attendanceViewModel.Attendance);
                        attendanceRepository.SaveChanges();
                        var att = attendanceRepository.getLatest(attendanceViewModel.Attendance.OfferedCourseID);
                        foreach (var studentAttendance in attendanceViewModel.studentAttendances)
                        {
                            studentAttendance.AttendanceID = att.AttendanceID;
                            studentAttendance.RegisteredCourseID = studentAttendance.RegisteredCourse.RegisteredCourseID;
                            studentAttendance.RegisteredCourse = null;
                        }
                        await studentAttendanceRepository.AddRange(attendanceViewModel.studentAttendances);
                        await studentAttendanceRepository.SaveChangesAsync();
                        msgType = Common.Success;
                        msg = Common.InsertSuccess;
                        ts.Complete();
                    }
                }
                else
                {

                    var result = "";
                    foreach (var values in ModelState.Values)
                        foreach (var error in values.Errors)
                            result += error.ErrorMessage + "\n";
                    return RedirectToAction("Attendance", new { Course = attendanceViewModel.Attendance.OfferedCourseID, w_Form = "create", _Action = "true", MessageType = Common.Error, Message = result });
                }
            }
            catch (Exception ex)
            {
                msgType = Common.Error;
                msg = Common.InsertFail;
            }
            return RedirectToAction("Attendance", new { Course = attendanceViewModel.Attendance.OfferedCourseID, MessageType = msgType, Message = msg });
        }
        [HttpGet]
        public async Task<IActionResult> AttendanceDetails(int? Course, int? ID)
        {
            var msgType = "";
            var msg = "";
            try
            {
                if (ID.HasValue)
                {
                    var obj = await attendanceRepository.GetById(ID.Value);
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
            return RedirectToAction("Attendance", new { Course = Course, MessageType = msgType, Message = msg });
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string isValid, int? Course, int? ID)
        {
            var msgType = "";
            var msg = "";
            try
            {
                if (!string.IsNullOrEmpty(isValid))
                    ViewBag.Message = "Invalid attempt.";

                if (ID.HasValue && Course.HasValue && await attendanceRepository.IsExists(ID.Value))
                {
                    var viewModel = new AttendanceViewModel
                    {
                        Attendance = await attendanceRepository.GetById(ID.Value),
                    };
                    foreach (var regCourse in await registeredCourseRepository.GetByCourse(Course.Value))
                    {
                        if (await studentAttendanceRepository.IsExists(regCourse.RegisteredCourseID, viewModel.Attendance.AttendanceID))
                            viewModel.studentAttendances.Add(await studentAttendanceRepository.GetByAttendance_RegId(viewModel.Attendance.AttendanceID, regCourse.RegisteredCourseID));
                        else
                            viewModel.studentAttendances.Add(new StudentAttendance() { RegisteredCourse = regCourse });
                    }
                    return PartialView(viewModel);
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

            return RedirectToAction("Attendance", new { Course = Course, MessageType = msgType, Message = msg });
        }
        [HttpPost]
        public async Task<IActionResult> Edit([Bind("Attendance,studentAttendances")] AttendanceViewModel attendanceViewModel)
        {
            var msgType = "";
            var msg = "";
            try
            {
                if (ModelState.IsValid)
                {
                    using (var ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        var tempNew = new List<StudentAttendance>();
                        var tempUpdate = new List<StudentAttendance>();
                        attendanceViewModel.Attendance.Last_Modified = DateTime.Now;
                        var user = accountRepository.GetUser(User.Identity.Name);
                        if (user != null)
                            attendanceViewModel.Attendance.User = user.Id;
                        attendanceRepository.Update(attendanceViewModel.Attendance);
                        attendanceRepository.SaveChanges();
                        foreach (var studentAttendance in attendanceViewModel.studentAttendances)
                        {
                            studentAttendance.AttendanceID = attendanceViewModel.Attendance.AttendanceID;
                            studentAttendance.RegisteredCourseID = studentAttendance.RegisteredCourse.RegisteredCourseID;
                            studentAttendance.RegisteredCourse = null;
                            if (await studentAttendanceRepository.IsExists(studentAttendance.StudentAttendanceID))
                                tempUpdate.Add(studentAttendance);
                            else
                                tempNew.Add(studentAttendance);
                        }
                        await Task.Run(() => studentAttendanceRepository.UpdateRange(tempUpdate));
                        await studentAttendanceRepository.AddRange(tempNew);
                        await studentAttendanceRepository.SaveChangesAsync();
                        msgType = Common.Success;
                        msg = Common.InsertSuccess;
                        ts.Complete();
                    }
                }
                else
                {
                    var result = "";
                    foreach (var values in ModelState.Values)
                        foreach (var error in values.Errors)
                            result += error.ErrorMessage + "\n";
                    return RedirectToAction("Attendance", new { Course = attendanceViewModel.Attendance.OfferedCourseID, w_Form = "edit", _Action = "true", MessageType = Common.Error, Message = result, ID = attendanceViewModel.Attendance.AttendanceID });
                }
            }
            catch (Exception ex)
            {
                msgType = Common.Error;
                msg = Common.Fail;
            }
            return RedirectToAction("Attendance", new { Course = attendanceViewModel.Attendance.OfferedCourseID, MessageType = msgType, Message = msg });
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? Course, int? ID)
        {
            var msgType = "";
            var msg = "";
            try
            {
                if (ID.HasValue && Course.HasValue)
                {
                    ViewBag.Course = Course.Value;
                    return PartialView(await attendanceRepository.GetById(ID.Value));
                }
                msgType = Common.Error;
                msg = Common.NotFound;
            }
            catch
            {
                msgType = Common.Error;
                msg = Common.Fail;
            }
            return RedirectToAction("Attendance", new { Course = Course, MessageType = msgType, Message = msg });
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Attendance attendance)
        {
            var msgType = "";
            var msg = "";
            try
            {
                if (await attendanceRepository.IsExists(attendance.AttendanceID))
                {
                    await attendanceRepository.Delete(attendance.AttendanceID);
                    await attendanceRepository.SaveChangesAsync();
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
            return RedirectToAction("Attendance", new { Course = attendance.OfferedCourseID, MessageType = msgType, Message = msg });
        }
        [HttpGet]
        public async Task<IActionResult> AttendanceSummary(int? Course)
        {
            var msgType = "";
            var msg = "";
            try
            {
                if (Course.HasValue && await offeredCourseRepository.IsExists(Course.Value))
                {
                    ViewBag.Course = await offeredCourseRepository.GetById(Course.Value);
                    var list = new List<StudentAttendanceSummaryViewModel>();
                    var totalHours = await attendanceRepository.GetTotalHoursMarked(Course.Value);
                    double totalAbsentHours = 0;
                    double totalPresentHours = 0;
                    double count = 0;
                    double tempPercentage = 0;
                    foreach (var regCourse in await registeredCourseRepository.GetByCourse(Course.Value))
                    {
                        var Obj = new StudentAttendanceSummaryViewModel
                        {
                            OfferedCourse = regCourse.OfferedCourse,
                            Students = regCourse.Student,
                        };
                        Obj.totalPresentHours = await studentAttendanceRepository.StudentAttendance(regCourse.RegisteredCourseID, true);
                        Obj.totalAbsentHours = await studentAttendanceRepository.StudentAttendance(regCourse.RegisteredCourseID, false);
                        totalPresentHours += Obj.totalPresentHours;
                        totalAbsentHours += Obj.totalAbsentHours;
                        if (totalHours > 0)
                            Obj.Percentage = (Obj.totalPresentHours / totalHours) * 100;
                        else
                            Obj.Percentage = 0;
                        tempPercentage += Obj.Percentage;
                        list.Add(Obj);
                        count += 1;
                    }
                    ViewBag.TotalPresentHours = totalPresentHours;
                    ViewBag.TotalAbsentHours = totalAbsentHours;
                    ViewBag.TotalHours = totalHours;
                    if (count > 0)
                    {
                        ViewBag.TotalPercentage = (tempPercentage / count).ToString() + "%";
                    }
                    else
                        ViewBag.TotalPercentage = "0 %";
                    return PartialView(list);
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
            return RedirectToAction("Attendance", new { Course = Course, MessageType = msgType, Message = msg });
        }
    }
}