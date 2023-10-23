using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Timetable_DateSheet_Generator.Data.DbContext;
using Timetable_DateSheet_Generator.Data.Repositories.Account;
using Timetable_DateSheet_Generator.Data.Repositories.Student;
using Timetable_DateSheet_Generator.Models;
using Timetable_DateSheet_Generator.Models.ViewModels;

namespace Timetable_DateSheet_Generator.Controllers.Student
{
    [Route("Student/StudentDashboard/[action]")]
    [Authorize(Roles = "Student")]
    public class StudentDashboardController : Controller
    {
        private readonly AccountRepository accountRepository;
        private readonly StudentRepository studentRepository;
        private readonly IHostingEnvironment hostingEnvironment;
        public StudentDashboardController(Timetable_DateSheet_Context timetable_DateSheet_Context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IHostingEnvironment _hostingEnvironment)
        {
            studentRepository=new StudentRepository(timetable_DateSheet_Context);
            hostingEnvironment = _hostingEnvironment;
            accountRepository = new AccountRepository(timetable_DateSheet_Context, userManager, signInManager);
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

                var user = await accountRepository.GetUserAsync(User.Identity.Name);
                DashboardViewModel dashboardViewModel = new DashboardViewModel
                {
                    profileView = user,
                };
                var student=await studentRepository.GetByUserId(user.Id);
                if(student!=null)
                {
                    ViewBag.Institute = student.Program.Department.Institute.InstituteName;
                    ViewBag.Department=student.Program.Department.DepartmentName;
                    ViewBag.Program = student.Program.ProgramName;
                }
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
                            var returnUrl = "~/Student/StudentDashboard/View?w_Form='pass'&_Action='true'&Message='Password changed successfully.'&MessageType='success'";
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