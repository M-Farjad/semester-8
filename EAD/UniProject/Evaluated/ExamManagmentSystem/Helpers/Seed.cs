using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace ExamManagmentSystem.Helpers
{
    public class Seed
    {
        public static async Task SeedRolesAndSuperAdminAsync(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var roles = new[] { "Clerk", "Admin", "SuperAdmin" };

            foreach (var roleName in roles)
            {
                var role = await roleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                    role = await roleManager.FindByNameAsync(roleName);
                }

                var currentClaims = await roleManager.GetClaimsAsync(role);
                var permissions = roleName switch
                {
                    "Clerk" => RolePermissions.ClerkPermissions,
                    "Admin" => RolePermissions.AdminPermissions,
                    "SuperAdmin" => RolePermissions.SuperAdminPermissions,
                    _ => new List<string>()
                };

                foreach (var claim in currentClaims.Where(c => c.Type == "Permission"))
                {
                    await roleManager.RemoveClaimAsync(role, claim);
                }

                foreach (var permission in permissions)
                {
                    await roleManager.AddClaimAsync(role, new Claim("Permission", permission));
                }
            }

            // Seed SuperAdmin user
            await CreateUserIfNotExists(
                userManager,
                email: "superadmin@example.com",
                password: "Admin@123",
                role: "SuperAdmin");
            await CreateUserIfNotExists(
                userManager,
                email: "farjad.waseem.fw@gmail.com",
                password: "Admin@123",
                role: "SuperAdmin");

            // Seed Admin user
            await CreateUserIfNotExists(
                userManager,
                email: "admin@example.com",
                password: "Admin@123",
                role: "Admin");

            // Seed Clerk user
            await CreateUserIfNotExists(
                userManager,
                email: "clerk@example.com",
                password: "Clerk@123",
                role: "Clerk");
        }

        private static async Task CreateUserIfNotExists(UserManager<ApplicationUser> userManager, string email, string password, string role)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                var newUser = new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(newUser, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(newUser, role);
                }
            }
        }
    }
}
