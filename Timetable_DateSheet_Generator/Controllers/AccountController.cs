using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.Transactions;
using Timetable_DateSheet_Generator.Data.DbContext;
using Timetable_DateSheet_Generator.Data.Repositories.Account;
using Timetable_DateSheet_Generator.Data.Repositories.Institute;
using Timetable_DateSheet_Generator.Data.Repositories.Time;
using Timetable_DateSheet_Generator.Models;
using Timetable_DateSheet_Generator.Models.ViewModels;

namespace Timetable_DateSheet_Generator.Controllers
{
    public class AccountController : Controller
    {
        private readonly InstituteRepository instituteRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly AccountRepository accountRepository;
        private readonly TimeRepository timeRepository;
        public AccountController(Timetable_DateSheet_Context timetable_DateSheet_Context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            instituteRepository = new InstituteRepository(timetable_DateSheet_Context);
            this.userManager = userManager;
            this.signInManager = signInManager;
            accountRepository = new AccountRepository(timetable_DateSheet_Context, userManager, signInManager);
            timeRepository = new TimeRepository(timetable_DateSheet_Context);
        }
        public IActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public async Task<IActionResult> Initialize()
        {
                                Console.WriteLine("An error occurred: " + "+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            try
            {
                                    Console.WriteLine("An error occurred: " + "+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
                timeRepository.Initialize();
                IdentityRole role1 = new IdentityRole()
                {
                    Id = "1",
                    Name = "Administrator",
                    NormalizedName = "administrator",
                    ConcurrencyStamp = "Administrator"
                };
                IdentityRole role2 = new IdentityRole()
                {
                    Id = "2",
                    Name = "Student",
                    NormalizedName = "student",
                    ConcurrencyStamp = "Student"
                };
                IdentityRole role3 = new IdentityRole()
                {
                    Id = "3",
                    Name = "Faculty",
                    NormalizedName = "faculty",
                    ConcurrencyStamp = "faculty"
                };
                if (!accountRepository.isRoleExists(role1))
                {
                    accountRepository.AddRole(role1);
                    accountRepository.SaveChanges();
                }
                if (!accountRepository.isRoleExists(role2))
                {
                    accountRepository.AddRole(role2);
                    accountRepository.SaveChanges();
                }
                if (!accountRepository.isRoleExists(role3))
                {
                    accountRepository.AddRole(role3);
                    accountRepository.SaveChanges();
                }

                using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {

                    var user = new ApplicationUser { Email = "Admin@email.com", UserName = "Admin@email.com", Name = "Admin" };    
                         Console.WriteLine("An error occurred: userssss " + user);
                    if(!accountRepository.isUserExists(user.Email))
                    {
                        var result = await accountRepository.CreateAsync(user, "Test123#");
                        if (result.Succeeded)
                        {
                            await accountRepository.SignInAsync(user, isPersistent: false);
                            await accountRepository.AddToRoleAsync(user, "Administrator");
                        }
                    }                                        
                        transactionScope.Complete();
                }
            }
            catch { }
            return RedirectToAction("Index", "Home");
        }
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return RedirectToAction("login");
        }        
        // [HttpPost]
        public async Task<IActionResult> logout()
        {
            accountRepository.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
        [AllowAnonymous]
        public IActionResult login(string returnUrl)
        {
            Common.returnUrl = returnUrl;
            return View(new LoginModel());
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var result = await accountRepository.PasswordSignInAsync(loginModel.Email, loginModel.Password, loginModel.RemeberMe, false);
                  
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(Common.returnUrl))
                    {
                        var URL = Common.returnUrl;
                        Common.returnUrl = null;
                        return Redirect(URL);
                    }
                    return RedirectToAction("index", "home");
                }
                else{
                      timeRepository.Initialize();
                IdentityRole role1 = new IdentityRole()
                {
                    Id = "1",
                    Name = "Administrator",
                    NormalizedName = "administrator",
                    ConcurrencyStamp = "Administrator"
                };
                IdentityRole role2 = new IdentityRole()
                {
                    Id = "2",
                    Name = "Student",
                    NormalizedName = "student",
                    ConcurrencyStamp = "Student"
                };
                IdentityRole role3 = new IdentityRole()
                {
                    Id = "3",
                    Name = "Faculty",
                    NormalizedName = "faculty",
                    ConcurrencyStamp = "faculty"
                };
                if (!accountRepository.isRoleExists(role1))
                {
                    accountRepository.AddRole(role1);
                    accountRepository.SaveChanges();
                }
                if (!accountRepository.isRoleExists(role2))
                {
                    accountRepository.AddRole(role2);
                    accountRepository.SaveChanges();
                }
                if (!accountRepository.isRoleExists(role3))
                {
                    accountRepository.AddRole(role3);
                    accountRepository.SaveChanges();
                }

                using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {

                    var user = new ApplicationUser { Email = "Admin@email.com", UserName = "Admin@email.com", Name = "Admin" };    
                         Console.WriteLine("An error occurred: userssss " + user);
                    if(!accountRepository.isUserExists(user.Email))
                    {
                        var resultC = await accountRepository.CreateAsync(user, "Test123#");
                        if (resultC.Succeeded)
                        {
                            await accountRepository.SignInAsync(user, isPersistent: false);
                            await accountRepository.AddToRoleAsync(user, "Administrator");
                        }
                    }                                        
                        transactionScope.Complete();
                }
                    ModelState.AddModelError(string.Empty, "Invalid Attempt!");
            }}
            return View(loginModel);
        }
        //[AllowAnonymous]
        //[HttpGet]
        //public IActionResult Register()
        //{
        //    return View();
        //}
        //[HttpPost]
        //[AllowAnonymous]
        //public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            var user = new ApplicationUser { Email = registerViewModel.UserEmail, UserName = registerViewModel.UserEmail, Name = registerViewModel.Name };
        //            var result = await accountRepository.CreateAsync(user, registerViewModel.Password);
        //            if (result.Succeeded)
        //            {
        //                await accountRepository.SignInAsync(user, isPersistent: false);
        //                /// Assign Role
        //                /// 
        //                await accountRepository.AddToRoleAsync(user, "Administrator");
        //                return RedirectToAction("Login", "Account");
        //            }
        //            foreach (var Error in result.Errors)
        //                ModelState.AddModelError("", Error.Description);
        //        }
        //    }
        //    catch
        //    {
        //        ModelState.AddModelError("", "Roles Not Found!");
        //    }
        //    return View(registerViewModel);
        //}

        private void Client_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}