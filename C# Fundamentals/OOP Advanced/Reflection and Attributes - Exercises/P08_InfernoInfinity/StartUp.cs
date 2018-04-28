using System;
using System.Linq;
using System.Reflection;

using Microsoft.Extensions.DependencyInjection;

public class StartUp
{
    static void Main()
    {
        var serviceProvider = ConfigureServices();

        var parametersFromServiceProvider = typeof(Engine)
            .GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
            .Where(f => f.FieldType.IsInterface)
            .Select(f => serviceProvider.GetService(f.FieldType))
            .ToArray();

        var engine = (Engine)Activator.CreateInstance(typeof(Engine), parametersFromServiceProvider);

        engine.Run();
    }

    private static IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        services.AddSingleton<IRepository, WeaponRepository>();
        services.AddTransient<IWeaponFactory, WeaponFactory>();
        services.AddTransient<IGemFactory, GemFactory>();
        services.AddTransient<ICommandInterpreter, CommandInterpreter>();
        services.AddTransient<IReader, ConsoleReader>();
        services.AddTransient<IWriter, ConsoleWriter>();

        var serviceProvider = services.BuildServiceProvider();
        return serviceProvider;
    }
}