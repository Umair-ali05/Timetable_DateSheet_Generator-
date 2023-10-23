using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timetable_DateSheet_Generator.Data.DbContext;
using Timetable_DateSheet_Generator.Models;
using Timetable_DateSheet_Generator.Models.ViewModels;

namespace Timetable_DateSheet_Generator.Data.Repositories.Account
{
    public class AccountRepository
    {
        private readonly Timetable_DateSheet_Context timetable_DateSheet_Context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        public AccountRepository(Timetable_DateSheet_Context timetable_DateSheet_Context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.timetable_DateSheet_Context = timetable_DateSheet_Context;
        }
        public async Task<ApplicationUser> GetUserByIDAsync(string id)
        {
            return
                await (timetable_DateSheet_Context.Users.Where(c => c.Id == id).FirstOrDefaultAsync());
        }
        public async void Register(ApplicationUser applicationUser)
        {

        }
        public async Task<List<RegisterViewModel>> getAllAdmins(string id)
        {
            var list = await timetable_DateSheet_Context.Users.Where(c => c.Id != id).ToListAsync();
            var admins = new List<RegisterViewModel>();
            var adminRole = timetable_DateSheet_Context.Roles.Where(c => c.Name.Trim().ToLower().Contains("administrator".Trim())).FirstOrDefault();
            if (adminRole != null)
            {
                foreach (var user in list)
                {
                    var temp = timetable_DateSheet_Context.UserRoles.Where(c => c.UserId == user.Id && c.RoleId == adminRole.Id).FirstOrDefault();
                    if (temp != null)
                        admins.Add(new RegisterViewModel() { ID = user.Id, Name = user.Name, UserEmail = user.Email, path = string.IsNullOrEmpty(user.Image) ? "" : user.Image });
                }
            }
            return admins;
        }
        public async void SignOutAsync()
        {
            await signInManager.SignOutAsync();
        }
        public async Task<SignInResult> PasswordSignInAsync(string Email, string Password, bool RemeberMe, bool lockoutOnFailure)
        {
            return await signInManager.PasswordSignInAsync(Email, Password, RemeberMe, lockoutOnFailure);
        }
        public async Task<IdentityResult> CreateAsync(ApplicationUser user, string password)
        {
            return await userManager.CreateAsync(user, password);
        }
        public async Task<IdentityResult> UpdateAsync(ApplicationUser user)
        {
            return await userManager.UpdateAsync(user);
        }
        public string GeneratePasswordHash(ApplicationUser user, string password)
        {
            return userManager.PasswordHasher.HashPassword(user, password);
        }
        public async Task<IdentityResult> ValidatePassword(ApplicationUser user, string pass)
        {
            var passwordValidator = new PasswordValidator<ApplicationUser>();
            return await passwordValidator.ValidateAsync(userManager, user, pass);
        }
        public void UpdateRecordAsync(ApplicationUser user)
        {
            timetable_DateSheet_Context.Users.Update(user);
        }
        public async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            await signInManager.SignInAsync(user, isPersistent: isPersistent);
        }
        public async Task<string> GetUserRole(ApplicationUser user)
        {
            var userRole = await timetable_DateSheet_Context.UserRoles.Where(c => c.UserId == user.Id).FirstOrDefaultAsync();
            if (userRole != null)
                return userRole.RoleId;
            return "";
        }
        public async Task<string> GetRole(string roleID)
        {
            var role = await timetable_DateSheet_Context.Roles.Where(c => c.Id == roleID).FirstOrDefaultAsync();
            if (role != null)
                return role.Name.ToLower();
            return "";
        }
        public async Task AddToRoleAsync(ApplicationUser user, string Role)
        {
            await userManager.AddToRoleAsync(user, Role);
        }
        public ApplicationUser GetUserByID(string id)
        {
            return
                (timetable_DateSheet_Context.Users.Where(c => c.Id == id).FirstOrDefault());
        }
        public async Task<ApplicationUser> GetUserAsync(string Email)
        {
            return
                await (timetable_DateSheet_Context.Users.Where(c => c.Email.Trim().ToLower().Equals(Email.Trim().ToLower())).FirstOrDefaultAsync());
        }
        public ApplicationUser GetUser(string Email)
        {
            return
                (timetable_DateSheet_Context.Users.Where(c => c.Email.Trim().ToLower().Equals(Email.Trim().ToLower())).FirstOrDefault());
        }
        public void AddRole(IdentityRole role)
        {
            timetable_DateSheet_Context.Roles.Add(role);
        }
        public async Task<bool> isRoleAssigned(string id)
        {
            return await timetable_DateSheet_Context.UserRoles.AnyAsync(c => c.UserId == id);
        }
        public bool isRoleExists(IdentityRole role)
        {
            return timetable_DateSheet_Context.Roles.Any(c => c.Name.Trim().ToLower().Equals(role.Name.Trim().ToLower()));
        }
        public void DeleteUser(string id)
        {
            timetable_DateSheet_Context.Users.Remove(GetUserByID(id));
        }
        public async void SaveChangesAsync()
        {
            await timetable_DateSheet_Context.SaveChangesAsync();
        }
        public void SaveChanges()
        {
            timetable_DateSheet_Context.SaveChanges();
        }
        public void DeleteAssignedRole(string id)
        {
            timetable_DateSheet_Context
                .UserRoles.RemoveRange(timetable_DateSheet_Context.UserRoles.Where(c => c.UserId == id).ToList());
        }
        public async Task<bool> isExists(string ID)
        {
            return await timetable_DateSheet_Context.Users.AnyAsync(c => c.Id == ID);
        }
        public bool isUserExists(string email)
        {
            return timetable_DateSheet_Context.Users.Any(c=>c.NormalizedEmail.Trim().ToLower().Equals(email.Trim().ToLower()));
        }
    }
}
