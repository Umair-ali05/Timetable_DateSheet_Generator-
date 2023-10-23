using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Transactions;
using Timetable_DateSheet_Generator.Data.DbContext;
using Timetable_DateSheet_Generator.Data.Repositories.Account;
using Timetable_DateSheet_Generator.Data.Repositories.Department;
using Timetable_DateSheet_Generator.Data.Repositories.Institute;
using Timetable_DateSheet_Generator.Data.Repositories.Program;
using Timetable_DateSheet_Generator.Data.Repositories.RegisteredCourse;
using Timetable_DateSheet_Generator.Data.Repositories.Student;
using Timetable_DateSheet_Generator.Models;
using Timetable_DateSheet_Generator.Models.ViewModels;

namespace Timetable_DateSheet_Generator.Controllers.Administration
{
    [Route("Administration/Students/[action]")]
    [Authorize(Roles = "Administrator")]
    public class StudentsController : Controller
    {
        private readonly AccountRepository accountRepository;
        private readonly InstituteRepository instituteRepository;
        private readonly DepartmentRepository departmentRepository;
        private readonly ProgramRepository programRepository;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly StudentRepository studentRepositoy;
        private readonly RegisteredCourseRepository registeredCourseRepository;
        public StudentsController(Timetable_DateSheet_Context timetable_DateSheet_Context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IHostingEnvironment _hostingEnvironment)
        {
            hostingEnvironment = _hostingEnvironment;
            accountRepository = new AccountRepository(timetable_DateSheet_Context, userManager, signInManager);
            instituteRepository = new InstituteRepository(timetable_DateSheet_Context);
            departmentRepository = new DepartmentRepository(timetable_DateSheet_Context);
            programRepository = new ProgramRepository(timetable_DateSheet_Context);
            studentRepositoy = new StudentRepository(timetable_DateSheet_Context);
            registeredCourseRepository = new RegisteredCourseRepository(timetable_DateSheet_Context);
        }
        [HttpGet]
        public async Task<IActionResult> Actions(string w_Form, string _Action, string Message, string MessageType, int? ID, string strID)
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
                    else if (w_Form.Trim().ToLower().Contains("pass") && !string.IsNullOrEmpty(strID))
                    {
                        ViewBag.ID = strID;
                        ViewBag.Action = "pass";
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
                return View(await studentRepositoy.GetAllView(null));
            }
            catch
            {
                ViewBag.MessageType = Common.Error;
                ViewBag.Message = Common.GetListFail;
                return View(new List<StudentViewModel>());
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
            return PartialView(new StudentViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("RegisterViewModel,Student")] StudentViewModel studentView)
        {
            var msg = "";
            var msgType = "";
            var errors = "";
            var folderPath = "";
            var imagePath = "";
            try
            {
                if (ModelState.IsValid)
                {

                    var user = accountRepository.GetUser(User.Identity.Name);
                    if (user != null)
                        studentView.Student.User = user.Id;
                    studentView.Student.Last_Modified = DateTime.Now;
                    if (studentView.RegisterViewModel.Image != null)
                    {
                        if (!Directory.Exists(Path.Combine(hostingEnvironment.WebRootPath, "Users/")))
                            Directory.CreateDirectory(imagePath);
                        folderPath = "Users/" + Guid.NewGuid().ToString() + studentView.RegisterViewModel.Image.FileName;
                        imagePath = Path.Combine(hostingEnvironment.WebRootPath, folderPath);
                        folderPath = "/" + folderPath;
                        await studentView.RegisterViewModel.Image.CopyToAsync(new FileStream(imagePath, FileMode.Create));
                    }
                    using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        ApplicationUser appUser = new ApplicationUser() { Name = studentView.RegisterViewModel.Name, Email = studentView.RegisterViewModel.UserEmail, UserName = studentView.RegisterViewModel.UserEmail, Image = folderPath };
                        var result = await accountRepository.CreateAsync(appUser, studentView.RegisterViewModel.Password);
                        if (result.Succeeded)
                        {
                            var temp = accountRepository.GetUser(studentView.RegisterViewModel.UserEmail);
                            if (temp != null)
                            {
                                studentView.Student.UserID = temp.Id;
                                accountRepository.DeleteAssignedRole(temp.Id);
                                await accountRepository.AddToRoleAsync(temp, "Student");
                                await studentRepositoy.Insert(studentView.Student);
                                await studentRepositoy.SaveChangesAsync();
                                msgType = Common.Success;
                                msg = Common.InsertSuccess;
                            }
                            else
                                errors += "User Not Found!";
                        }
                        else
                        {
                            foreach (var Error in result.Errors)
                                errors += Error.Description + " \n";
                        }
                        transactionScope.Complete();
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
            if (string.IsNullOrEmpty(errors))
                return RedirectToAction("Actions", new { MessageType = msgType, Message = msg });
            return RedirectToAction("Actions", new { w_Form = "create", _Action = "true", MessageType = Common.Error, Message = errors });

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

                if (ID.HasValue && await studentRepositoy.IsExists(ID.Value))
                {
                    var Obj = await studentRepositoy.GetById(ID.Value);
                    ViewData["Institutes"] = new SelectList(instituteRepository.GetForSelectList(), "ID", "Name", Obj.Program.Department.InstituteID);
                    ViewData["Departments"] = new SelectList(departmentRepository.GetForSelectList(Obj.Program.Department.InstituteID), "ID", "Name", Obj.Program.DepartmentID);
                    ViewData["Programs"] = new SelectList(programRepository.GetForSelectList(Obj.Program.DepartmentID), "ID", "Name", Obj.ProgarmID);
                    if (!string.IsNullOrEmpty(Obj.UserID))
                    {
                        var user = accountRepository.GetUserByID(Obj.UserID);
                        return PartialView(new StudentViewModel() { Student = Obj, RegisterViewModel = new RegisterViewModel() { Name = user.Name, UserEmail = user.Email, Password = user.PasswordHash, ConfirmPassword = user.PasswordHash } });
                    }
                    else
                    {
                        return PartialView(new StudentViewModel() { Student = Obj, RegisterViewModel = new RegisterViewModel() });
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
        [HttpPost]
        public async Task<IActionResult> Edit([Bind("RegisterViewModel,Student")] StudentViewModel studentView)
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

                    var user = accountRepository.GetUser(User.Identity.Name);
                    if (user != null)
                        studentView.Student.User = user.Id;
                    studentView.Student.Last_Modified = DateTime.Now;
                    if (studentView.RegisterViewModel.Image != null)
                    {
                        if (!Directory.Exists(Path.Combine(hostingEnvironment.WebRootPath, "Users/")))
                            Directory.CreateDirectory(imagePath);
                        folderPath = "Users/" + Guid.NewGuid().ToString() + studentView.RegisterViewModel.Image.FileName;
                        imagePath = Path.Combine(hostingEnvironment.WebRootPath, folderPath);
                        folderPath = "/" + folderPath;
                        await studentView.RegisterViewModel.Image.CopyToAsync(new FileStream(imagePath, FileMode.Create));
                    }
                    var appUser = accountRepository.GetUserByID(studentView.Student.UserID);
                    if (!string.IsNullOrEmpty(folderPath) && studentView.RegisterViewModel.Image != null)
                        appUser.Image = folderPath;
                    appUser.Email = studentView.RegisterViewModel.UserEmail;
                    appUser.Name = studentView.RegisterViewModel.Name;
                    appUser.UserName = studentView.RegisterViewModel.UserEmail;
                    using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        var result = await accountRepository.UpdateAsync(appUser);
                        if (result.Succeeded)
                        {
                            studentRepositoy.Update(studentView.Student);
                            studentRepositoy.SaveChanges();
                            if (!await accountRepository.isRoleAssigned(studentView.Student.UserID))
                                await accountRepository.AddToRoleAsync(accountRepository.GetUserByID(studentView.Student.UserID), "Student");
                            accountRepository.SaveChanges();
                            msgType = Common.Success;
                            msg = Common.UpdateSuccess;
                        }
                        else
                        {
                            foreach (var Error in result.Errors)
                                errors += Error.Description + " \n";
                        }
                        transactionScope.Complete();
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
            if (string.IsNullOrEmpty(errors))
                return RedirectToAction("Actions", new { MessageType = msgType, Message = msg });
            return RedirectToAction("Actions", new { w_Form = "edit", _Action = "true", MessageType = Common.Error, Message = errors, ID = studentView.Student.StudentID });
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
                    var obj = await studentRepositoy.GetById(ID.Value);
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
                    if (string.IsNullOrEmpty(obj.UserID))
                    {
                        ViewBag.FName = "";
                        ViewBag.FEmail = "";
                        ViewBag.FPic = "";
                    }
                    else
                    {
                        var user = await accountRepository.GetUserByIDAsync(obj.UserID);
                        ViewBag.FName = user.Name;
                        ViewBag.FEmail = user.Email;
                        ViewBag.FPic = user.Image;
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
                {
                    var student = await studentRepositoy.GetById(ID.Value);
                    var user = accountRepository.GetUserByID(student.User);
                    ViewBag.Name = user.Name;
                    ViewBag.Email = user.Email;
                    return PartialView(new StudentViewModel() { Student = student, RegisterViewModel = new RegisterViewModel() { Name = user.Name, UserEmail = user.Email } });
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
        public async Task<IActionResult> Delete([Bind("RegisterViewModel,Student")] StudentViewModel studentView)
        {
            var msgType = "";
            var msg = "";
            try
            {
                if (await studentRepositoy.IsExists(studentView.Student.StudentID))
                {
                    using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        var regCourses = await registeredCourseRepository.GetByStudent(studentView.Student.StudentID);
                        await Task.Run(() => registeredCourseRepository.RemoveRange(regCourses));
                        await registeredCourseRepository.SaveChangesAsync();
                        if (await accountRepository.isRoleAssigned(studentView.Student.UserID))
                            accountRepository.DeleteAssignedRole(studentView.Student.UserID);
                        if (await accountRepository.isExists(studentView.Student.UserID))
                            accountRepository.DeleteUser(studentView.Student.UserID);
                        accountRepository.SaveChanges();
                        await studentRepositoy.Delete(studentView.Student.StudentID);
                        await studentRepositoy.SaveChangesAsync();
                        msgType = Common.Success;
                        msg = Common.DeleteSuccess;
                        transactionScope.Complete();
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
        [HttpGet]
        public async Task<IActionResult> ChangePassword(string isValid, string ID)
        {
            var msgType = "";
            var msg = "";
            try
            {
                if (!string.IsNullOrEmpty(isValid))
                    ViewBag.Message = "Invalid Attempt.";

                if (!string.IsNullOrEmpty(ID) && await accountRepository.isExists(ID))
                {
                    var Obj = accountRepository.GetUserByID(ID);
                    var registerViewModel = new RegisterViewModel() { Name = Obj.Name, UserEmail = Obj.Email };
                    return PartialView(registerViewModel);
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
        public async Task<IActionResult> ChangePassword([Bind("UserEmail,Password,ConfirmPassword,Name")] RegisterViewModel registerViewModel)
        {
            var msg = "";
            var msgType = "";
            var errors = "";
            var tempID = "";
            try
            {
                var temp = accountRepository.GetUser(registerViewModel.UserEmail);
                tempID = temp.Id;
                if (ModelState.IsValid)
                {
                    var passResult = await accountRepository.ValidatePassword(temp, registerViewModel.Password);
                    if (passResult.Succeeded)
                    {
                        temp.PasswordHash = accountRepository.GeneratePasswordHash(temp, registerViewModel.Password);
                        var result = await accountRepository.UpdateAsync(temp);
                        if (result.Succeeded)
                        {
                            accountRepository.SaveChanges();
                            msgType = Common.Success;
                            msg = Common.UpdateSuccess;
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
            if (string.IsNullOrEmpty(errors))
                return RedirectToAction("Actions", new { MessageType = msgType, Message = msg });
            return RedirectToAction("Actions", new { w_Form = "pass", _Action = "true", MessageType = Common.Error, Message = errors, strID = tempID });

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
    }
}