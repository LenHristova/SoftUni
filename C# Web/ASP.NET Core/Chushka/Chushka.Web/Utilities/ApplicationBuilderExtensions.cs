namespace Chushka.Web.Utilities
{
    using Data;
    using Data.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class ApplicationBuilderExtensions
    {
        private static readonly string[] RoleNames = {"Admin", "User"};

        public static async void SeedDatabaseAsync(this IApplicationBuilder app)
        {
            var serviceFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            var scope = serviceFactory.CreateScope();
            using (scope)
            {
                var db = scope.ServiceProvider.GetRequiredService<ChushkaDbContext>();
                db.Database.Migrate();

                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

                foreach (var roleName in RoleNames)
                {
                    if (!await roleManager.RoleExistsAsync(roleName))
                    {
                        await roleManager.CreateAsync(new IdentityRole(roleName));
                    }

                    var admin = await userManager.FindByNameAsync(configuration["DatabaseSeed:AdminUsername"]);

                    if (admin == null)
                    {
                        admin = new User
                        {
                            UserName = configuration["DatabaseSeed:AdminUsername"],
                            Email = configuration["DatabaseSeed:AdminEmail"],
                            FullName = configuration["DatabaseSeed:AdminFullName"],
                        };

                        var result = await userManager.CreateAsync(admin, configuration["DatabaseSeed:AdminPassword"]);

                        if (result.Succeeded)
                        {
                            result = await userManager.AddToRoleAsync(admin, "Admin");
                        }
                    }
                }
            }
        }
    }
}
