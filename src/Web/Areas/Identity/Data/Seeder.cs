using Microsoft.AspNetCore.Identity;

namespace Web.Areas.Identity.Data
{
    public class Seeder
    {
        public async static Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            var roles = new[]
            {
                "Admin",
                "User"
            };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        public async static Task SeedAdminUser(UserManager<IdentityUser> userManager)
        {
            var adminEmail = "admin@gmail.com";
            var adminPassword = "admin123";

            var user = await userManager.FindByEmailAsync(adminEmail);
            if (user == null)
            {
                user = new IdentityUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(user, adminPassword);
                await userManager.AddToRoleAsync(user, "Admin");
            }
        }
    }
}
