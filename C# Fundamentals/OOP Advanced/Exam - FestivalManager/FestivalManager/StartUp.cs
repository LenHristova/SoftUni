using System;
using System.Collections.Generic;

using FestivalManager.Entities.Factories;
using FestivalManager.Entities.Factories.Contracts;

using Microsoft.Extensions.DependencyInjection;

namespace FestivalManager
{
    using Core;
    using Core.Contracts;
    using Core.Controllers;
    using Core.Controllers.Contracts;
    using Core.IO;
    using Core.IO.Contracts;

    using Entities;
    using Entities.Contracts;

    public static class StartUp
    {
        public static void Main(string[] args)
        {
            //Stage stage = new Stage(new List<ISet>(),new List<ISong>(), new List<IPerformer>() );
            //IFestivalController festivalController = new FestivalController(stage);
            //ISetController setController = new SetController(stage);

            //var engine = new Engine(festivalController, setController);

            //engine.Запали();


            var serviceProvider = ConfigureServices();

            var engine = serviceProvider.GetService<IEngine>();
            engine.Run();
        }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton<IReader, ConsoleReader>();
            services.AddSingleton<IWriter, ConsoleWriter>();

            services.AddSingleton<IEngine, Engine>();
            services.AddSingleton<IStage, Stage>();

            services.AddSingleton<ISetController, SetController>();
            services.AddSingleton<IFestivalController, FestivalController>();

            services.AddTransient<IInstrumentFactory, InstrumentFactory>();
            services.AddTransient<IPerformerFactory, PerformerFactory>();
            services.AddTransient<ISetFactory, SetFactory>();
            services.AddTransient<ISongFactory, SongFactory>();

            services.AddTransient<IList<ISet>, List<ISet>>();
            services.AddTransient<IList<ISong>, List<ISong>>();
            services.AddTransient<IList<IPerformer>, List<IPerformer>>();

            return services.BuildServiceProvider();
        }
    }
}