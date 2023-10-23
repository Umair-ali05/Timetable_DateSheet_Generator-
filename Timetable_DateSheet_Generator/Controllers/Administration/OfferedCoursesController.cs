using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Timetable_DateSheet_Generator.Data.DbContext;
using Timetable_DateSheet_Generator.Data.Repositories.Account;
using Timetable_DateSheet_Generator.Data.Repositories.Department;
using Timetable_DateSheet_Generator.Data.Repositories.FacultyMember;
using Timetable_DateSheet_Generator.Data.Repositories.Institute;
using Timetable_DateSheet_Generator.Data.Repositories.OfferedCourse;
using Timetable_DateSheet_Generator.Data.Repositories.Program;
using Timetable_DateSheet_Generator.Data.Repositories.Semester;
using Timetable_DateSheet_Generator.Models;

namespace Timetable_DateSheet_Generator.Controllers.Administration
{
    [Route("Administration/OfferedCourses/[action]")]
    [Authorize(Roles = "Administrator")]
    public class OfferedCoursesController : Controller
    {
        private readonly AccountRepository accountRepository;
        private readonly InstituteRepository instituteRepository;
        private readonly ProgramRepository programRepository;
        private readonly DepartmentRepository departmentRepository;
        private readonly SemesterRepository semesterRepository;
        private readonly OfferedCourseRepository offeredCourseRepository;
        private readonly FacultyMemberRepository facultyMemberRepository;
        public OfferedCoursesController(Timetable_DateSheet_Context timetable_DateSheet_Context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            facultyMemberRepository = new FacultyMemberRepository(timetable_DateSheet_Context);
            accountRepository = new AccountRepository(timetable_DateSheet_Context, userManager, signInManager);
            instituteRepository = new InstituteRepository(timetable_DateSheet_Context);
            departmentRepository = new DepartmentRepository(timetable_DateSheet_Context);
            programRepository = new ProgramRepository(timetable_DateSheet_Context);
            semesterRepository = new SemesterRepository(timetable_DateSheet_Context);
            offeredCourseRepository = new OfferedCourseRepository(timetable_DateSheet_Context);
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
                return View(await offeredCourseRepository.GetAll(null));
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
            ViewData["Semesters"] = new SelectList(semesterRepository.GetForSelectList(), "ID", "Name");
            ViewData["FacultyMembers"] = new SelectList(facultyMemberRepository.GetForSelectList(null), "ID", "Name");
            return PartialView();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("OfferedCourseID,FacultyMemberID,DepartmentID,ProgramID,SemesterID,OfferedCourseTitle,OfferedCourseCreditHours,OfferedCourseContactHours,OfferedCourseCategory,OfferedCourseSpecial,OfferedCourseSection,OfferedCourseSemesterNo")] OfferedCourses OfferedCourse)
        {
            var msg = "";
            var msgType = "";
            try
            {
                if (ModelState.IsValid)
                {
                    var user = accountRepository.GetUser(User.Identity.Name);
                    if (user != null)
                        OfferedCourse.User = user.Id;
                    OfferedCourse.Last_Modified = DateTime.Now;
                    await offeredCourseRepository.Insert(OfferedCourse);
                    await offeredCourseRepository.SaveChangesAsync();
                    msgType = Common.Success;
                    msg = Common.InsertSuccess;
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

                if (ID.HasValue && await offeredCourseRepository.IsExists(ID.Value))
                {
                    var obj = await offeredCourseRepository.GetById(ID.Value);
                    ViewData["Institutes"] = new SelectList(instituteRepository.GetForSelectList(), "ID", "Name", obj.Department.InstituteID);
                    ViewData["Departments"] = new SelectList(departmentRepository.GetForSelectList(obj.Program.Department.InstituteID), "ID", "Name", obj.Program.DepartmentID);
                    ViewData["Departments1"] = new SelectList(departmentRepository.GetForSelectList(obj.Department.InstituteID), "ID", "Name", obj.DepartmentID);
                    ViewData["Programs"] = new SelectList(programRepository.GetForSelectList(obj.Program.Department.InstituteID, obj.Program.DepartmentID), "ID", "Name", obj.ProgramID);
                    ViewData["Semesters"] = new SelectList(semesterRepository.GetForSelectList(), "ID", "Name", obj.SemesterID);
                    ViewData["FacultyMembers"] = new SelectList(facultyMemberRepository.GetForSelectList(obj.Department.InstituteID), "ID", "Name", obj.FacultyMemberID);
                    return PartialView(obj);
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
        public async Task<IActionResult> Edit([Bind("OfferedCourseID,FacultyMemberID,DepartmentID,ProgramID,SemesterID,OfferedCourseTitle,OfferedCourseCreditHours,OfferedCourseContactHours,OfferedCourseCategory,OfferedCourseSpecial,OfferedCourseSection,OfferedCourseSemesterNo")] OfferedCourses OfferedCourse)
        {
            var msgType = "";
            var msg = "";
            try
            {
                if (ModelState.IsValid)
                {
                    if (await offeredCourseRepository.IsExists(OfferedCourse.OfferedCourseID))
                    {
                        var user = accountRepository.GetUser(User.Identity.Name);
                        if (user != null)
                            OfferedCourse.User = user.Id;
                        OfferedCourse.Last_Modified = DateTime.Now;
                        offeredCourseRepository.Update(OfferedCourse);
                        await offeredCourseRepository.SaveChangesAsync();
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
                    var result = "";
                    foreach (var values in ModelState.Values)
                        foreach (var error in values.Errors)
                            result += error.ErrorMessage + "\n";
                    return RedirectToAction("Actions", new { w_Form = "edit", _Action = "true", MessageType = Common.Error, Message = result, ID = OfferedCourse.OfferedCourseID });
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
                    return PartialView(await offeredCourseRepository.GetById(ID.Value));
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
        public async Task<IActionResult> Delete([Bind("OfferedCourseID,FacultyMemberID,DepartmentID,ProgramID,SemesterID,OfferedCourseTitle,OfferedCourseCreditHours,OfferedCourseContactHours,OfferedCourseCategory,OfferedCourseSpecial,OfferedCourseSection,OfferedCourseSemesterNo")] OfferedCourses OfferedCourse)
        {
            var msgType = "";
            var msg = "";
            try
            {
                if (await (offeredCourseRepository.IsExists(OfferedCourse.OfferedCourseID)))
                {
                    await offeredCourseRepository.Delete(OfferedCourse.OfferedCourseID);
                    await offeredCourseRepository.SaveChangesAsync();
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
            return Json(programRepository.GetForSelectList(string.IsNullOrEmpty(institute) ? (int?)null : Convert.ToInt32(institute), string.IsNullOrEmpty(department) ? (int?)null : Convert.ToInt32(department)));
        }
        public JsonResult getFaculties(string institute)
        {
            return Json(facultyMemberRepository.GetForSelectList(string.IsNullOrEmpty(institute) ? (int?)null : Convert.ToInt32(institute)));
        }
    }
}