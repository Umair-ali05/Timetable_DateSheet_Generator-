using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;
using Timetable_DateSheet_Generator.Data.DbContext;
using Timetable_DateSheet_Generator.Data.Repositories.Account;
using Timetable_DateSheet_Generator.Data.Repositories.Time;
using Timetable_DateSheet_Generator.Models;

namespace Timetable_DateSheet_Generator.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly AccountRepository accountRepository;
        private readonly TimeRepository timeRepository;
        public HomeController(Timetable_DateSheet_Context timetable_DateSheet_Context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            accountRepository = new AccountRepository(timetable_DateSheet_Context, userManager, signInManager);
            timeRepository = new TimeRepository(timetable_DateSheet_Context);
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                if (User != null)
                {
                    var user = accountRepository.GetUser(User.Identity.Name.ToLower());
                    if (user != null)
                    {
                        var userRole = await accountRepository.GetUserRole(user);
                        if (!string.IsNullOrEmpty(userRole))
                        {
                            var role = await accountRepository.GetRole(userRole);
                            if (!string.IsNullOrEmpty(role))
                            {
                                if (role.ToLower().Contains("administrator"))
                                    return RedirectToAction("View", "Dashboard", new { Message = "Welcome! " + user.Name, MessageType = "success" });
                                else if (role.ToLower().Contains("student"))
                                    return RedirectToAction("View", "StudentDashboard", new { Message = "Welcome! " + user.Name, MessageType = "success" });
                                else if (role.ToLower().Contains("faculty"))
                                    return RedirectToAction("View", "FacultyDashboard", new { Message = "Welcome! " + user.Name, MessageType = "success" });

                                else;
                            }
                        }
                    }
                }
            }
            catch
            {
            }
            return NotFound();
        }
        public IActionResult Initialize()
        {
            try
            {
                timeRepository.Initialize();
               
            }
            catch { }
            return RedirectToAction("Index");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
