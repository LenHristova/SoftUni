using System;

using Microsoft.Extensions.DependencyInjection;

public class Program
{
    public static void Main(string[] args)
    {
        var serviceProvider = ConfigureServices();

        var engine = serviceProvider.GetService<Engine>();
        engine.Run();
    }

    private static IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        services.AddSingleton<IReader, ConsoleReader>();
        services.AddSingleton<IWriter, ConsoleWriter>();

        services.AddSingleton<Engine>();
        services.AddSingleton<ICommandInterpreter, CommandInterpreter>();

        services.AddSingleton<IHarvesterController, HarvesterController>();
        services.AddSingleton<IProviderController, ProviderController>();

        services.AddSingleton<ICommandFactory, CommandFactory>();
        services.AddTransient<IHarvesterFactory, HarvesterFactory>();
        services.AddTransient<IProviderFactory, ProviderFactory>();

        services.AddSingleton<IEnergyRepository, EnergyRepository>();

        return services.BuildServiceProvider();
    }
}