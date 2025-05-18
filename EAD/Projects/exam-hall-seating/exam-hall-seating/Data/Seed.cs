using exam_hall_seating.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace exam_hall_seating.Data
{
    public class Seed
    {
        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();

                if (!await roleManager.RoleExistsAsync(AppRole.Admin))
                    await roleManager.CreateAsync(new AppRole { Name = AppRole.Admin });
                if (!await roleManager.RoleExistsAsync(AppRole.Instructor))
                    await roleManager.CreateAsync(new AppRole { Name = AppRole.Instructor });

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

                var newAdminUser = new AppUser()
                {
                    UserName = "admin",
                    Email = "newadmin@gmail.com",
                    EmailConfirmed = true,
                    PhoneNumber = "5004003020",
                    PhoneNumberConfirmed = true,
                    FirstName = "firstadmin",
                    LastName = "lastadminlast"
                };
                var result = await userManager.CreateAsync(newAdminUser, "Test@123456");
                if(result.Succeeded)
                {
                    await userManager.AddToRoleAsync(newAdminUser, AppRole.Admin);
                }
                else
                {
                    foreach(var error in result.Errors)
                    {
                        Console.WriteLine($"Hata Kodu: {error.Code}");
                        Console.WriteLine($"Hata Açıklaması: {error.Description}");
                        Console.WriteLine();
                    }
                }

                var newAppUser = new AppUser()
                {
                    UserName = "farjad",
                    Email = "farjad@gmail.com",
                    EmailConfirmed = true,
                    PhoneNumber = "5005005050",
                    PhoneNumberConfirmed = true,
                    FirstName = "farjad",
                    LastName = "waseem",
                    DepartmentId = 1
                };


                var result2 = await userManager.CreateAsync(newAppUser, "Test@123456");
                if (result2.Succeeded)
                {
                    await userManager.AddToRoleAsync(newAppUser, AppRole.Instructor);
                }
                else
                {
                    foreach (var error in result2.Errors)
                    {
                        Console.WriteLine($"Error Code: {error.Code}");
                        Console.WriteLine($"Error Description: {error.Description}");
                        Console.WriteLine();
                    }
                }
                

            }
        }
    }
}
