using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Microsoft.Extensions.DependencyInjection;

using P04_WorkForce.Contracts;
using P04_WorkForce.Core;
using P04_WorkForce.Factories;
using P04_WorkForce.IO;

namespace P04_WorkForce
{
    class StartUp
    {
        static void Main()
        {
            var serviceProvider = ConfigureServices();
            var engine = SetUpEngine(serviceProvider);
            engine.Run();
        }

        private static Engine SetUpEngine(IServiceProvider serviceProvider)
        {
            var parametersFromServiceProvider = typeof(Engine)
                .GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .Where(f => f.FieldType.IsInterface)
                .Select(f => serviceProvider.GetService(f.FieldType))
                .ToArray();

            var engine = (Engine)Activator.CreateInstance(typeof(Engine), parametersFromServiceProvider);
            return engine;
        }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddTransient<IReader, ConsoleReader>();
            services.AddTransient<IWriter, ConsoleWriter>();

            services.AddTransient<ITaskManager, TaskManager>();
            services.AddTransient<IEmployeeManager, EmployeeManager>();

            services.AddTransient<ICollection<IEmployee>, List<IEmployee>>();
            services.AddTransient<ICollection<IJob>, List<IJob>>();

            services.AddTransient<IEmployeeFactory, EmployeeFactory>();
            services.AddTransient<IJobFactory, JobFactory>();

            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider;
        }

    }
}
