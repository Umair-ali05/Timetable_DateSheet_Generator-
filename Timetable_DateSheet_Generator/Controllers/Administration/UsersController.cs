using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Transactions;
using Timetable_DateSheet_Generator.Data.DbContext;
using Timetable_DateSheet_Generator.Data.Repositories.Account;
using Timetable_DateSheet_Generator.Models;
using Timetable_DateSheet_Generator.Models.ViewModels;

namespace Timetable_DateSheet_Generator.Controllers.Administration
{
    [Route("Administration/Users/[action]")]
    [Authorize(Roles = "Administrator")]
    public class UsersController : Controller
    {
        private readonly AccountRepository accountRepository;
        private readonly IHostingEnvironment hostingEnvironment;
        public UsersController(Timetable_DateSheet_Context timetable_DateSheet_Context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IHostingEnvironment _hostingEnvironment)
        {
            hostingEnvironment = _hostingEnvironment;
            accountRepository = new AccountRepository(timetable_DateSheet_Context, userManager, signInManager);
        }
        [HttpGet]
        public async Task<IActionResult> Actions(string w_Form, string _Action, string Message, string MessageType, string ID, string strID)
        {
            try
            {
                if (!string.IsNullOrEmpty(_Action) && _Action.Trim().ToLower().Contains("true")
                    && !string.IsNullOrEmpty(w_Form))
                {
                    if (w_Form.Trim().ToLower().Contains("create"))
                        ViewBag.Action = "Create";
                    else if (w_Form.Trim().ToLower().Contains("edit") && !string.IsNullOrEmpty(ID))
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
                var user = accountRepository.GetUser(User.Identity.Name);
                return View(await accountRepository.getAllAdmins(user.Id));
            }
            catch
            {
                ViewBag.MessageType = Common.Error;
                ViewBag.Message = Common.GetListFail;
                return View(new List<RegisterViewModel>());
            }
        }
        [HttpGet]
        public IActionResult Create(string isNew)
        {
            if (!string.IsNullOrEmpty(isNew))
                ViewBag.Message = "Invalid Attempt";
            return PartialView(new RegisterViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Create(RegisterViewModel registerView)
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
                    if (registerView.Image != null)
                    {
                        if (!Directory.Exists(Path.Combine(hostingEnvironment.WebRootPath, "Users/")))
                            Directory.CreateDirectory(imagePath);
                        folderPath = "Users/" + Guid.NewGuid().ToString() + registerView.Image.FileName;
                        imagePath = Path.Combine(hostingEnvironment.WebRootPath, folderPath);
                        folderPath = "/" + folderPath;
                        await registerView.Image.CopyToAsync(new FileStream(imagePath, FileMode.Create));
                    }
                    using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        ApplicationUser appUser = new ApplicationUser() { Name = registerView.Name, Email = registerView.UserEmail, UserName = registerView.UserEmail, Image = string.IsNullOrEmpty(folderPath) ? null : folderPath };
                        var result = await accountRepository.CreateAsync(appUser, registerView.Password);
                        if (result.Succeeded)
                        {
                            var temp = accountRepository.GetUser(registerView.UserEmail);
                            if (temp != null)
                            {
                                accountRepository.DeleteAssignedRole(temp.Id);
                                await accountRepository.AddToRoleAsync(temp, "Administrator");
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
        public async Task<IActionResult> Edit(string isValid, string ID)
        {
            var msgType = "";
            var msg = "";
            try
            {
                if (!string.IsNullOrEmpty(isValid))
                    ViewBag.Message = "Invalid attempt.";

                if (!string.IsNullOrEmpty(ID) && await accountRepository.isExists(ID))
                {
                    var user = accountRepository.GetUserByID(ID);
                    return PartialView(new RegisterViewModel() { ID = user.Id, Name = user.Name, UserEmail = user.Email, Password = user.PasswordHash, ConfirmPassword = user.PasswordHash });
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
        public async Task<IActionResult> Edit([Bind("ID,UserEmail,Password,ConfirmPassword,Name,Image")] RegisterViewModel registerView)
        {
            var msg = "";
            var msgType = "";
            var errors = "";
            var tempID = "";
            var imagePath = "";
            var folderPath = "";
            try
            {
                if (ModelState.IsValid)
                {

                    if (registerView.Image != null)
                    {
                        if (!Directory.Exists(Path.Combine(hostingEnvironment.WebRootPath, "Users/")))
                            Directory.CreateDirectory(imagePath);
                        folderPath = "Users/" + Guid.NewGuid().ToString() + registerView.Image.FileName;
                        imagePath = Path.Combine(hostingEnvironment.WebRootPath, folderPath);
                        folderPath = "/" + folderPath;
                        await registerView.Image.CopyToAsync(new FileStream(imagePath, FileMode.Create));
                    }
                    var appUser = accountRepository.GetUserByID(registerView.ID);
                    if (!string.IsNullOrEmpty(folderPath) && registerView.Image != null)
                        appUser.Image = folderPath;
                    tempID = appUser.Id;
                    appUser.Email = registerView.UserEmail;
                    appUser.Name = registerView.Name;
                    appUser.UserName = registerView.UserEmail;
                    using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        var result = await accountRepository.UpdateAsync(appUser);
                        if (result.Succeeded)
                        {
                            if (!await accountRepository.isRoleAssigned(appUser.Id))
                                await accountRepository.AddToRoleAsync(appUser, "Administrator");
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
            return RedirectToAction("Actions", new { w_Form = "edit", _Action = "true", MessageType = Common.Error, Message = errors, ID = tempID });
        }
        [HttpGet]
        public async Task<IActionResult> Details(string ID)
        {
            var msgType = "";
            var msg = "";
            try
            {
                if (!string.IsNullOrEmpty(ID))
                {
                    var user = await accountRepository.GetUserByIDAsync(ID);
                    return PartialView(new RegisterViewModel() { Name = user.Name, UserEmail = user.Email, path = user.Image });
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
        public async Task<IActionResult> Delete(string ID)
        {
            var msgType = "";
            var msg = "";
            try
            {
                if (!string.IsNullOrEmpty(ID))
                {
                    var user = await accountRepository.GetUserByIDAsync(ID);
                    return PartialView(new RegisterViewModel() { ID = user.Id, Name = user.Name, UserEmail = user.Email });
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
        public async Task<IActionResult> Delete([Bind("ID,UserEmail,Password,ConfirmPassword,Name,Image")] RegisterViewModel registerView)
        {
            var msgType = "";
            var msg = "";
            try
            {
                var obj = await accountRepository.GetUserByIDAsync(registerView.ID);
                if (obj != null)
                {
                    using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        accountRepository.DeleteAssignedRole(obj.Id);
                        accountRepository.DeleteUser(obj.Id);
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
                    var registerViewModel = new RegisterViewModel() { ID = Obj.Id, Name = Obj.Name, UserEmail = Obj.Email };
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
        public async Task<IActionResult> ChangePassword([Bind("ID,UserEmail,Password,ConfirmPassword,Name")] RegisterViewModel registerViewModel)
        {
            var msg = "";
            var msgType = "";
            var errors = "";
            var tempID = "";
            try
            {
                var temp = accountRepository.GetUserByID(registerViewModel.ID);
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
    }
}