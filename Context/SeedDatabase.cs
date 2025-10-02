using Microsoft.AspNetCore.Identity;

namespace test_app.Context
{
    public class SeedDatabase
    {
        public static async void Initialize(IApplicationBuilder app)
        {
            var userManager = app.ApplicationServices
                                .CreateScope()
                                .ServiceProvider
                                .GetRequiredService<UserManager<AppUser>>();

            var roleManager = app.ApplicationServices
                                .CreateScope()
                                .ServiceProvider
                                .GetRequiredService<RoleManager<AppRole>>();

            if (!roleManager.Roles.Any())
            {
                var admin = new AppRole { Name = "Admin" };
                await roleManager.CreateAsync(admin);
            }

            if (!userManager.Users.Any())
            {
                var admin = new AppUser
                {
                    FullName = "Rana ÇITIR",
                    UserName = "rcitir",
                    Email = "rcitir@artisoft.com.tr"
                };

                await userManager.CreateAsync(admin, "12345678");
                await userManager.AddToRoleAsync(admin, "Admin");

            }

        }
    }
}
