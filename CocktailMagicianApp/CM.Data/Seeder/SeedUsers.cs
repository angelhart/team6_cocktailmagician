using CM.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Data.Seeder
{
    public static class SeedUsers
    {
        public static async Task Initialize(IServiceProvider serviceProvider, string testUserPw)
        {
            using (var context = new CMContext(
                                 serviceProvider.GetRequiredService<DbContextOptions<CMContext>>()))
            {
                // For testing purposes seed all with the same password.
                // Password is set with the following:
                // dotnet user-secrets set SeedUserPW <pw>
                // The admin user can do anything

                var magicianID = await EnsureUser(serviceProvider, testUserPw, "magician@cocktails.com");
                await EnsureRole(serviceProvider, magicianID, "Magician");

                // allowed user can create and edit contacts that they create
                var crawlerID1 = await EnsureUser(serviceProvider, testUserPw, "crawler1@cocktails.com");
                await EnsureRole(serviceProvider, crawlerID1, "Crawler");

                var crawlerID2 = await EnsureUser(serviceProvider, testUserPw, "crawler2@cocktails.com");
                await EnsureRole(serviceProvider, crawlerID2, "Crawler");

                SeedDB(context, magicianID);
            }

            static async Task<Guid> EnsureUser(IServiceProvider serviceProvider, string testUserPw, string UserName)
            {
                var userManager = serviceProvider.GetService<UserManager<AppUser>>();

                var user = await userManager.FindByNameAsync(UserName);
                if (user == null)
                {
                    user = new AppUser
                    {
                        UserName = UserName,
                        EmailConfirmed = true,
                        LockoutEnabled = false,
                    };
                    await userManager.CreateAsync(user, testUserPw);
                }

                if (user == null)
                {
                    throw new Exception("The password is probably not strong enough!");
                }

                return user.Id;
            }

            static async Task<IdentityResult> EnsureRole(IServiceProvider serviceProvider, Guid uid, string role)
            {
                IdentityResult IR = null;
                var roleManager = serviceProvider.GetService<RoleManager<AppRole>>();

                if (roleManager == null)
                {
                    throw new Exception("roleManager null");
                }

                if (!await roleManager.RoleExistsAsync(role))
                {
                    IR = await roleManager.CreateAsync(new AppRole(role));
                }

                var userManager = serviceProvider.GetService<UserManager<AppUser>>();

                var user = await userManager.FindByIdAsync(uid.ToString());

                if (user == null)
                {
                    throw new Exception("The testUserPw password was probably not strong enough!");
                }

                IR = await userManager.AddToRoleAsync(user, role);

                return IR;
            }

            static void SeedDB(CMContext context, Guid adminID)
            {
                if (context.Users.Any(x => x.UserName.Contains("magician")))
                {
                    return;   // DB has been seeded with the managers
                }

                context.SaveChanges();
            }
        }
    }
}
