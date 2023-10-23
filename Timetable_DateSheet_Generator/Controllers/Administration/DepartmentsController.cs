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
using Timetable_DateSheet_Generator.Data.Repositories.Institute;
using Timetable_DateSheet_Generator.Models;

namespace Timetable_DateSheet_Generator.Controllers.Administration
{
    [Route("Administration/Departments/[action]")]
    [Authorize(Roles = "Administrator")]
    public class DepartmentsController : Controller
    {
        private readonly AccountRepository accountRepository;
        private readonly DepartmentRepository departmentRepository;
        private readonly InstituteRepository instituteRepository;
        public DepartmentsController(Timetable_DateSheet_Context timetable_DateSheet_Context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            accountRepository = new AccountRepository(timetable_DateSheet_Context, userManager, signInManager);
            departmentRepository = new DepartmentRepository(timetable_DateSheet_Context);
            instituteRepository = new InstituteRepository(timetable_DateSheet_Context);
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
                return View(await departmentRepository.GetAll(null));
            }
            catch
            {
                ViewBag.MessageType = Common.Error;
                ViewBag.Message = Common.GetListFail;
                return View(new List<Departments>());
            }
        }
        [HttpGet]
        public IActionResult Create(string isNew)
        {
            if (!string.IsNullOrEmpty(isNew))
                ViewBag.Message = "Invalid Attempt";
            ViewData["Institutes"] = new SelectList(instituteRepository.GetForSelectList(), "ID", "Name");
            return PartialView();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("DepartmentID,DepartmentName,InstituteID")] Departments Department)
        {
            var msg = "";
            var msgType = "";
            try
            {
                if (ModelState.IsValid)
                {
                    var user = accountRepository.GetUser(User.Identity.Name);
                    if (user != null)
                        Department.User = user.Id;
                    Department.Last_Modified = DateTime.Now;
                    await departmentRepository.Insert(Department);
                    await departmentRepository.SaveChangesAsync();
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

                if (ID.HasValue && await departmentRepository.IsExists(ID.Value))
                {
                    var Obj = await departmentRepository.GetById(ID.Value);
                    ViewData["Institutes"] = new SelectList(instituteRepository.GetForSelectList(), "ID", "Name", Obj.InstituteID);
                    return PartialView(Obj);
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
        public async Task<IActionResult> Edit([Bind("DepartmentID,DepartmentName,InstituteID")] Departments Department)
        {
            var msgType = "";
            var msg = "";
            try
            {
                if (ModelState.IsValid)
                {
                    if (await departmentRepository.IsExists(Department.DepartmentID))
                    {
                        var user = accountRepository.GetUser(User.Identity.Name);
                        if (user != null)
                            Department.User = user.Id;
                        Department.Last_Modified = DateTime.Now;
                        departmentRepository.Update(Department);
                        await departmentRepository.SaveChangesAsync();
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
                    return RedirectToAction("Actions", new { w_Form = "edit", _Action = "true", MessageType = Common.Error, Message = result, ID = Department.DepartmentID });
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
                    var obj = await departmentRepository.GetById(ID.Value);
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
                else
                {
                    msgType = Common.Error;
                    msg = Common.NotFound;
                }
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
                    return PartialView(await departmentRepository.GetById(ID.Value));
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
        public async Task<IActionResult> Delete([Bind("DepartmentID,DepartmentName,InstituteID")] Departments Department)
        {
            var msgType = "";
            var msg = "";
            try
            {
                if (await departmentRepository.IsExists(Department.DepartmentID))
                {
                    await departmentRepository.Delete(Department.DepartmentID);
                    await departmentRepository.SaveChangesAsync();
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
    }
}