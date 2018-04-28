using System;

    public class StartUp
    {
        static void Main()
        {
            //var serviceProvider = ConfigureServices();
            var strategyFactory = new StrategyFactory();
            var calculator = new PrimitiveCalculator(strategyFactory);
            var reader = new ConsoleReader();
            var writer = new ConsoleWriter();
            var engine = new Engine(calculator, reader, writer);
            engine.Run();
        }

    //    private static IServiceProvider ConfigureServices()
    //    {
    //        var services = new ServiceCollection();

    //        services.AddSingleton<ICalculator, PrimitiveCalculator>();
    //        services.AddSingleton<IStrategyFactory, StrategyFactory>();
    //        services.AddSingleton<IReader, ConsoleReader>();
    //        services.AddSingleton<IWriter, ConsoleWriter>();

    //        var serviceProvider = services.BuildServiceProvider();
    //        return serviceProvider;
    //    }
    //}
}