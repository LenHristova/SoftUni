using System;

using Microsoft.Extensions.DependencyInjection;

using P03_BarraksWars.Contracts;
using P03_BarraksWars.Core;
using P03_BarraksWars.Core.Factories;
using P03_BarraksWars.Data;

namespace P03_BarraksWars
{
    class AppEntryPoint
    {
        static void Main(string[] args)
        {
            var serviceProvider = ConfigureServices();

            ICommandInterpreter commandInterpreter = new CommandInterpreter(serviceProvider);
            IRunnable engine = new Engine(commandInterpreter);
            engine.Run();
        }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton<IRepository, UnitRepository>();
            services.AddTransient<IUnitFactory, UnitFactory>();

            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider;
        }
    }
}
