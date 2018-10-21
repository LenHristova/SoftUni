namespace SoftUniCopy.Web.Utilities
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using SoftUniCopy.Models;

    public static class ApplicationBuilderExtensions
    {
        private const string DefaultAdminUsername = "admin";
        private const string DefaultAdminEmail = "admin@example.com";
        private const string DefaultAdminPassword = "admin";

        private static readonly IdentityRole[] roles =
        {
            new IdentityRole("Administrator"),
            new IdentityRole("Lecturer"),
        };

        public static async void SeedDatabaseAsync(this IApplicationBuilder app)
        {
            var serviceFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            var scope = serviceFactory.CreateScope();
            using (scope)
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role.Name))
                    {
                        await roleManager.CreateAsync(role);
                    }

                    var admin = await userManager.FindByNameAsync(DefaultAdminUsername);

                    if (admin == null)
                    {
                        admin = new User
                        {
                            UserName = DefaultAdminUsername,
                            Email = DefaultAdminEmail
                        };

                        var result = await userManager.CreateAsync(admin, DefaultAdminPassword);

                        if (result.Succeeded)
                        {
                            result = await userManager.AddToRoleAsync(admin, roles[0].Name);
                        }
                    }
                }
            }
        }
    }
}
