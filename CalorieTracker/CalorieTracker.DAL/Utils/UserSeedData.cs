using CalorieTracker.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieTracker.DAL.Utils
{
    public class UserSeedData : ISeedData
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserSeedData(UserManager<ApplicationUser> userManager)
        {
            _userManager=userManager;
        }
        public async Task DataSeed()
        {
            if (!await _userManager.Users.AnyAsync())
            {
                var admin = new ApplicationUser
                {
                    UserName ="admin",
                    Email = "admin@gmail.com",
                    FullName = "System Admin",
                    EmailConfirmed=true
                };

                var user = new ApplicationUser
                {
                    UserName ="user",
                    Email = "user@gmail.com",
                    FullName = "Normal User",
                    EmailConfirmed=true
                };

                var superAdmin = new ApplicationUser
                {
                    UserName ="superAdmin",
                    Email = "superAdmin@gmail.com",
                    FullName = "System SuperAdmin",
                    EmailConfirmed=true
                };

                await _userManager.CreateAsync(superAdmin, "Pass@1122");
                await _userManager.CreateAsync(admin,"Pass@1122");
                await _userManager.CreateAsync(user,"Pass@1122");

                await _userManager.AddToRoleAsync(superAdmin, "SuperAdmin");
                await _userManager.AddToRoleAsync(admin, "Admin");
                await _userManager.AddToRoleAsync(user, "User");

            }
        }
    }
}
