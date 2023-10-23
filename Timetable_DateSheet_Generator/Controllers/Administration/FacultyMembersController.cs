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
using Timetable_DateSheet_Generator.Data.Repositories.FacultyMember;
using Timetable_DateSheet_Generator.Data.Repositories.Institute;
using Timetable_DateSheet_Generator.Data.Repositories.OfferedCourse;
using Timetable_DateSheet_Generator.Models;
using Timetable_DateSheet_Generator.Models.ViewModels;

namespace Timetable_DateSheet_Generator.Controllers.Administration
{
    [Route("Administration/FacultyMembers/[action]")]
    [Authorize(Roles = "Administrator")]
    public class FacultyMembersController : Controller
    {
        private readonly AccountRepository accountRepository;
        private readonly InstituteRepository instituteRepository;
        private readonly DepartmentRepository departmentRepository;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly FacultyMemberRepository facultyRepository;
        private readonly OfferedCourseRepository offeredCourseRepository;
        public FacultyMembersController(Timetable_DateSheet_Context timetable_DateSheet_Context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IHostingEnvironment _hostingEnvironment)
        {
            hostingEnvironment = _hostingEnvironment;
            accountRepository = new AccountRepository(timetable_DateSheet_Context, userManager, signInManager);
            instituteRepository = new InstituteRepository(timetable_DateSheet_Context);
            facultyRepository = new FacultyMemberRepository(timetable_DateSheet_Context);
            departmentRepository = new DepartmentRepository(timetable_DateSheet_Context);
            offeredCourseRepository = new OfferedCourseRepository(timetable_DateSheet_Context);
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
                return View(await facultyRepository.GetAllView());
            }
            catch
            {
                ViewBag.MessageType = Common.Error;
                ViewBag.Message = Common.GetListFail;
                return View(new List<FacultyViewModel>());
            }
        }
        [HttpGet]
        public IActionResult Create(string isNew)
        {
            if (!string.IsNullOrEmpty(isNew))
                ViewBag.Message = "Invalid Attempt";
            ViewData["Institutes"] = new SelectList(instituteRepository.GetForSelectList(), "ID", "Name");
            ViewData["Departments"] = new SelectList(departmentRepository.GetForSelectList(null), "ID", "Name");
            return PartialView(new FacultyViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("registerViewModel,FacultyMember")] FacultyViewModel facultyView)
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
                        facultyView.FacultyMember.User = user.Id;
                    facultyView.FacultyMember.Last_Modified = DateTime.Now;
                    if (facultyView.registerViewModel.Image != null)
                    {
                        if (!Directory.Exists(Path.Combine(hostingEnvironment.WebRootPath, "Users/")))
                            Directory.CreateDirectory(imagePath);
                        folderPath = "Users/" + Guid.NewGuid().ToString() + facultyView.registerViewModel.Image.FileName;
                        imagePath = Path.Combine(hostingEnvironment.WebRootPath, folderPath);
                        folderPath = "/" + folderPath;
                        await facultyView.registerViewModel.Image.CopyToAsync(new FileStream(imagePath, FileMode.Create));
                    }
                    ApplicationUser appUser = new ApplicationUser() { Name = facultyView.registerViewModel.Name, Email = facultyView.registerViewModel.UserEmail, UserName = facultyView.registerViewModel.UserEmail, Image = folderPath };
                    using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        var result = await accountRepository.CreateAsync(appUser, facultyView.registerViewModel.Password);
                        if (result.Succeeded)
                        {
                            var temp = accountRepository.GetUser(facultyView.registerViewModel.UserEmail);
                            if (temp != null)
                            {
                                facultyView.FacultyMember.UserID = temp.Id;
                                accountRepository.DeleteAssignedRole(temp.Id);
                                await accountRepository.AddToRoleAsync(temp, "Faculty");
                                await facultyRepository.Insert(facultyView.FacultyMember);
                                await facultyRepository.SaveChangesAsync();
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

                if (ID.HasValue && await facultyRepository.IsExists(ID.Value))
                {
                    var Obj = await facultyRepository.GetById(ID.Value);
                    var user = accountRepository.GetUserByID(Obj.UserID);
                    ViewData["Institutes"] = new SelectList(instituteRepository.GetForSelectList(), "ID", "Name", Obj.Department.InstituteID);
                    ViewData["Departments"] = new SelectList(departmentRepository.GetForSelectList(Obj.Department.InstituteID), "ID", "Name", Obj.DepartmentID);
                    return PartialView(new FacultyViewModel() { FacultyMember = Obj, registerViewModel = new RegisterViewModel() { Name = user.Name, UserEmail = user.Email, Password = user.PasswordHash, ConfirmPassword = user.PasswordHash } });
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
        public async Task<IActionResult> Edit([Bind("registerViewModel,FacultyMember")] FacultyViewModel facultyView)
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
                        facultyView.FacultyMember.User = user.Id;
                    facultyView.FacultyMember.Last_Modified = DateTime.Now;
                    if (facultyView.registerViewModel.Image != null)
                    {
                        if (!Directory.Exists(Path.Combine(hostingEnvironment.WebRootPath, "Users/")))
                            Directory.CreateDirectory(imagePath);
                        folderPath = "Users/" + Guid.NewGuid().ToString() + facultyView.registerViewModel.Image.FileName;
                        imagePath = Path.Combine(hostingEnvironment.WebRootPath, folderPath);
                        folderPath = "/" + folderPath;
                        await facultyView.registerViewModel.Image.CopyToAsync(new FileStream(imagePath, FileMode.Create));
                    }
                    var appUser = accountRepository.GetUserByID(facultyView.FacultyMember.UserID);
                    if (!string.IsNullOrEmpty(folderPath) && facultyView.registerViewModel.Image != null)
                        appUser.Image = folderPath;
                    appUser.Email = facultyView.registerViewModel.UserEmail;
                    appUser.Name = facultyView.registerViewModel.Name;
                    appUser.UserName = facultyView.registerViewModel.UserEmail;
                    using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        var result = await accountRepository.UpdateAsync(appUser);
                        if (result.Succeeded)
                        {
                            facultyRepository.Update(facultyView.FacultyMember);
                            if (!await accountRepository.isRoleAssigned(facultyView.FacultyMember.UserID))
                                await accountRepository.AddToRoleAsync(accountRepository.GetUserByID(facultyView.FacultyMember.UserID), "Faculty");
                            await facultyRepository.SaveChangesAsync();
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
            return RedirectToAction("Actions", new { w_Form = "edit", _Action = "true", MessageType = Common.Error, Message = errors, ID = facultyView.FacultyMember.FacultyMemberID });
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
                    var obj = await facultyRepository.GetById(ID.Value);
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
                    var faculty = await facultyRepository.GetById(ID.Value);
                    var user = accountRepository.GetUserByID(faculty.User);
                    ViewBag.Name = user.Name;
                    ViewBag.Email = user.Email;
                    return PartialView(new FacultyViewModel() { FacultyMember = faculty, registerViewModel = new RegisterViewModel() { Name = user.Name, UserEmail = user.Email } });
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
        public async Task<IActionResult> Delete([Bind("registerViewModel,FacultyMember")] FacultyViewModel facultyView)
        {
            var msgType = "";
            var msg = "";
            try
            {
                if (await facultyRepository.IsExists(facultyView.FacultyMember.FacultyMemberID))
                {
                    using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        var courses = await offeredCourseRepository.GetByFacultyId(facultyView.FacultyMember.FacultyMemberID);
                        await Task.Run(() => offeredCourseRepository.RemoveRange(courses));
                        await offeredCourseRepository.SaveChangesAsync();
                        if (await accountRepository.isRoleAssigned(facultyView.FacultyMember.UserID))
                            accountRepository.DeleteAssignedRole(facultyView.FacultyMember.UserID);
                        if (await accountRepository.isExists(facultyView.FacultyMember.UserID))
                            accountRepository.DeleteUser(facultyView.FacultyMember.UserID);
                        await facultyRepository.Delete(facultyView.FacultyMember.FacultyMemberID);
                        await facultyRepository.SaveChangesAsync();
                        accountRepository.SaveChanges();
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
    }
}