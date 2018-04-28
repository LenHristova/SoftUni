using System;

using BashSoft.Contracts;
using BashSoft.Factories;
using BashSoft.IO;
using BashSoft.Judge;
using BashSoft.Repository;

using Microsoft.Extensions.DependencyInjection;

namespace BashSoft
{
    public class Launcher
    {
        static void Main()
        {
            var serviceProvider = ConfigureServices();

            var reader = serviceProvider.GetService<IReader>();
            reader.StartReadingCommands();
        }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton<IDatabase, StudentsRepository>();
            services.AddTransient<IDataFilter, RepositoryFilter>();
            services.AddTransient<IDataSorter, RepositorySorter>();
            services.AddSingleton<ICommandFactory, CommandFactory>();
            services.AddSingleton<IReader, InputReader>();
            services.AddSingleton<IInterpreter, CommandInterpreter>();
            services.AddTransient<IDirectoryManager, IOManager>();
            services.AddTransient<IContentComparer, Tester>();

            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider;
        }
    }
}