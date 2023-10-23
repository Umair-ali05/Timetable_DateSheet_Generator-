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
using Timetable_DateSheet_Generator.Data.Repositories.Building;
using Timetable_DateSheet_Generator.Data.Repositories.Institute;
using Timetable_DateSheet_Generator.Data.Repositories.OfferedCourseTimeSlot;
using Timetable_DateSheet_Generator.Data.Repositories.Room;
using Timetable_DateSheet_Generator.Data.Repositories.RoomAvailibility;
using Timetable_DateSheet_Generator.Models;

namespace Timetable_DateSheet_Generator.Controllers.Administration
{
    [Route("Administration/Rooms/[action]")]
    [Authorize(Roles = "Administrator")]
    public class RoomsController : Controller
    {
        private readonly AccountRepository accountRepository;
        private readonly RoomRepository roomRepository;
        private readonly BuildingRepository buildingRepository;
        private readonly InstituteRepository instituteRepository;
        private readonly RoomAvailibilityRepository roomAvailibilityRepository;
        private readonly CourseTimeSlotRepository courseTimeSlotRepository;
        public RoomsController(Timetable_DateSheet_Context timetable_DateSheet_Context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            accountRepository = new AccountRepository(timetable_DateSheet_Context, userManager, signInManager);
            instituteRepository = new InstituteRepository(timetable_DateSheet_Context);
            buildingRepository = new BuildingRepository(timetable_DateSheet_Context);
            roomRepository = new RoomRepository(timetable_DateSheet_Context);
            roomAvailibilityRepository = new RoomAvailibilityRepository(timetable_DateSheet_Context);
            courseTimeSlotRepository = new CourseTimeSlotRepository(timetable_DateSheet_Context);
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
                return View(await roomRepository.GetAll(null));
            }
            catch
            {
                ViewBag.MessageType = Common.Error;
                ViewBag.Message = Common.GetListFail;
                return View(new List<Rooms>());
            }
        }
        [HttpGet]
        public IActionResult Create(string isNew)
        {
            if (!string.IsNullOrEmpty(isNew))
                ViewBag.Message = "Invalid Attempt";
            ViewData["Institutes"] = new SelectList(instituteRepository.GetForSelectList(), "ID", "Name");
            ViewData["Buildings"] = new SelectList(buildingRepository.GetForSelectList(null), "ID", "Name");
            return PartialView();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("BuildingID,RoomID,RoomName,RoomType,SeatingCapacity")] Rooms Room)
        {
            var msg = "";
            var msgType = "";
            try
            {
                if (ModelState.IsValid)
                {
                    var user = accountRepository.GetUser(User.Identity.Name);
                    if (user != null)
                        Room.User = user.Id;
                    Room.Last_Modified = DateTime.Now;
                    await roomRepository.Insert(Room);
                    await roomRepository.SaveChangesAsync();
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

                if (ID.HasValue && await roomRepository.IsExists(ID.Value))
                {
                    var Obj = await roomRepository.GetById(ID.Value);
                    ViewData["Institutes"] = new SelectList(instituteRepository.GetForSelectList(), "ID", "Name", Obj.Building.InstituteID);
                    ViewData["Buildings"] = new SelectList(buildingRepository.GetForSelectList(Obj.Building.InstituteID), "ID", "Name", Obj.BuildingID);
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
        public async Task<IActionResult> Edit([Bind("BuildingID,RoomID,RoomName,RoomType,SeatingCapacity")] Rooms Room)
        {
            var msgType = "";
            var msg = "";
            try
            {
                if (ModelState.IsValid)
                {
                    if (await roomRepository.IsExists(Room.RoomID))
                    {
                        var user = accountRepository.GetUser(User.Identity.Name);
                        if (user != null)
                            Room.User = user.Id;
                        Room.Last_Modified = DateTime.Now;
                        roomRepository.Update(Room);
                        await roomRepository.SaveChangesAsync();
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
                    return RedirectToAction("Actions", new { w_Form = "edit", _Action = "true", MessageType = Common.Error, Message = result, ID = Room.RoomID });
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
                    var obj = await roomRepository.GetById(ID.Value);
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
                    ViewBag.Date = string.Format("{0:hh:mm:ss tt}", obj.Last_Modified);
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
                    return PartialView(await roomRepository.GetById(ID.Value));
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
        public async Task<IActionResult> Delete([Bind("BuildingID,RoomID,RoomName,RoomType,SeatingCapacity")] Rooms Room)
        {
            var msgType = "";
            var msg = "";
            try
            {
                using (var ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    if (await roomRepository.IsExists(Room.RoomID))
                    {
                        var roomTimings = await roomAvailibilityRepository.getByRoom(Room.RoomID);
                        await Task.Run(() => roomAvailibilityRepository.RemoveRange(roomTimings));
                        await roomAvailibilityRepository.SaveChangesAsync();
                        var courseTimings = await courseTimeSlotRepository.GetByRoom(Room.RoomID);
                        await Task.Run(() => courseTimeSlotRepository.RemoveRange(courseTimings));
                        await courseTimeSlotRepository.SaveChangesAsync();
                        await roomRepository.Delete(Room.RoomID);
                        await roomRepository.SaveChangesAsync();
                        msgType = Common.Success;
                        msg = Common.DeleteSuccess;
                    }
                    else
                    {
                        msgType = Common.Error;
                        msg = Common.NotFound;
                    }
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                msgType = Common.Error;
                msg = Common.Fail;
            }
            return RedirectToAction("Actions", new { MessageType = msgType, Message = msg });
        }
        //[HttpGet]
        //public IActionResult getLists(int? Institute, int? Building)
        //{
        //    //var institutes = new SelectList(this.instituteRepository.GetForSelectList(), "ID", "Name",Institute.HasValue? Institute.Value.ToString():string.Empty).ToList();
        //    //institutes.Insert(0, (new SelectListItem { Text = string.Empty, Value = string.Empty }));           

        //    var institutes = new SelectList(null, "ID", "Name").ToList();
        //    var item = new SelectListItem() { Text = string.Empty, Value = string.Empty };
        //    institutes.Insert(0, item);
        //    institutes.AddRange(new SelectList(instituteRepository.GetForSelectList(), "ID", "Name", Institute.HasValue ? Institute.Value.ToString() : string.Empty).ToList());
        //    ViewData["Institutes"] = institutes;

        //    var buildings = new SelectList(null, "ID", "Name").ToList();
        //    buildings.Insert(0, item);
        //    buildings.AddRange(new SelectList(buildingRepository.GetForSelectList(Institute), "ID", "Name", Building.HasValue ? Building.Value.ToString() : string.Empty).ToList());
        //    ViewData["Buildings"] = institutes;

        //    return PartialView();
        //}
        public JsonResult getBuildings(string institute)
        {
            //if(!string.IsNullOrEmpty(institute))
            //    return this.Json(new SelectList(this.buildingRepository.GetForSelectList(Convert.ToInt32(institute)), "ID", "Name"));
            //else
            //    return this.Json(new SelectList(this.buildingRepository.GetForSelectList(null), "ID", "Name"));
            if (!string.IsNullOrEmpty(institute))
                return Json(buildingRepository.GetForSelectList(Convert.ToInt32(institute)));
            else
                return Json(buildingRepository.GetForSelectList(null));
        }
    }
}