namespace Employees.App
{
    using System;
    using AutoMapper;
    using Core;
    using Core.Contracts;
    using Data;
    using IO;
    using IO.Contracts;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Services;
    using Services.Contracts;

    public class StartUp
    {
        public static void Main()
        {

            var serviceProvider = ConfigureServices();

            var engine = serviceProvider.GetService<IEngine>();
            engine.Run();
        }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddDbContext<EmployeesContext>(options =>
                    options.UseSqlServer(Config.ConnectionString));

            services.AddAutoMapper(cfg => cfg.AddProfile<EmployeesProfile>());

            services.AddSingleton<IEngine, Engine>();
            services.AddTransient<IDbInitializerService, DbInitializerService>();
            services.AddTransient<IReader, ConsoleReader>();
            services.AddTransient<IWriter, ConsoleWriter>();
            services.AddTransient<ICommandParser, CommandParser>();
            services.AddTransient<IEmployeeService, EmployeeService>();

            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider;
        }
    }
}
