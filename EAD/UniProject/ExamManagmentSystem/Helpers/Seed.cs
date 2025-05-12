using Microsoft.AspNetCore.Identity;

namespace ExamManagmentSystem.Helpers
{
    public class Seed
    {
        public static async Task SeedRolesAndAdmin(IServiceProvider service)
        {
            var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = service.GetRequiredService<UserManager<ApplicationUser>>();

            string[] roles = { "Clerk", "Admin", "SuperAdmin" };

            foreach (var role in roles)
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));

            // Create Super Admin
            string email = "superadmin@exam.com";
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                user = new ApplicationUser { UserName = email, Email = email };
                await userManager.CreateAsync(user, "SuperAdmin123!");
                await userManager.AddToRoleAsync(user, "SuperAdmin");
            }
        }

    }

}
