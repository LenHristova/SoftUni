//namespace SoftUniCopy.Web.Common
//{
//    using System;
//    using Data;
//    using Microsoft.AspNetCore.Hosting;
//    using Microsoft.AspNetCore.Identity;
//    using Microsoft.EntityFrameworkCore;
//    using Microsoft.Extensions.DependencyInjection;
//    using Microsoft.Extensions.Logging;
//    using SoftUniCopy.Models;

//    public class DbInitializer
//    {
//        private const string DefaultAdminUsername = "admin";
//        private const string DefaultAdminEmail = "admin";
//        private const string DefaultAdminPassword = "admin@example.com";

//        private static readonly IdentityRole[] roles =
//        {
//            new IdentityRole("Administrator"),
//            new IdentityRole("Lecturer"),
//        };

//        public static async void SeedDatabase(IWebHost host)
//        {
//            using (var scope = host.Services.CreateScope())
//            {
//                var services = scope.ServiceProvider;

//                try
//                {
//                    var context = services.GetRequiredService<SoftUniCopyContext>();
//                    context.Database.Migrate();

//                    var userManager = services.GetRequiredService<UserManager<User>>();
//                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

//                    foreach (var role in roles)
//                    {
//                        if (!await roleManager.RoleExistsAsync(role.Name))
//                        {
//                            var result = await roleManager.CreateAsync(role);
//                        }

//                        var admin = await userManager.FindByNameAsync(DefaultAdminUsername);

//                        if (admin == null)
//                        {
//                            admin = new User
//                            {
//                                UserName = DefaultAdminUsername,
//                                Email = DefaultAdminEmail
//                            };

//                            var result = await userManager.CreateAsync(admin, DefaultAdminPassword);

//                            result = await userManager.AddToRoleAsync(admin, roles[0].Name);
//                        }
//                    }
//                }
//                catch (Exception ex)
//                {
//                    var logger = services.GetRequiredService<ILogger<Program>>();
//                    logger.LogError(ex, "An error occurred seeding the DB.");
//                }
//            }
            
//        }
//    }
//}
