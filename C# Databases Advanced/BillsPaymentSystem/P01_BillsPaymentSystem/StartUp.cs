using Microsoft.EntityFrameworkCore;

namespace P01_BillsPaymentSystem
{
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using Contracts;
    using Contracts.Core;
    using Contracts.Factories;
    using Contracts.Models;
    using Contracts.Services;
    using Core;
    using Data;
    using Factories;
    using Initializer;
    using Models;
    using Services;

    public class StartUp
    {
        public static void Main()
        {
            var serviceProvider = ConfigureServices();

            DatabaseInitializer.ResetDatabase();

            var engine = serviceProvider.GetService<IEngine>();
            engine.Run();
        }

        private static IServiceProvider ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<IEngine, Engine>();
            services.AddDbContext<BillsPaymentSystemContext>(options => 
                options
                    .UseLazyLoadingProxies()
                    .UseSqlServer(Config.ConnectionString));
            services.AddSingleton<IMainController, MainController>();
            services.AddSingleton<ISession, Session>();
            services.AddSingleton<IUserService, UserService>();
            services.AddTransient<IReader, ConsoleReader>();
            services.AddTransient<IWriter, ConsoleWriter>();
            services.AddTransient<ICommandFactory, CommandFactory>();
            services.AddTransient<IMenuFactory, MenuFactory>();
 

            IServiceProvider serviceProvider = services.BuildServiceProvider();
            return serviceProvider;
        }

    }
}
