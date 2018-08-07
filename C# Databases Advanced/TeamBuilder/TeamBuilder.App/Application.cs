namespace TeamBuilder.App
{
    using System;
    using Core;
    using Core.Contracts;
    using Core.IO;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Services;
    using Services.Contracts;

    public class Application
    {
        public static void Main()
        {
            var serviceProvider = ConfigureService();

            var engine = serviceProvider.GetService<IEngine>();
            engine.Run();
        }

        private static IServiceProvider ConfigureService()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection
                .AddDbContext<TeamBuilderContext>(
                    optionsBuilder => 
                        optionsBuilder
                            .UseLazyLoadingProxies()
                            .UseSqlServer(Config.ConnectionString));

            serviceCollection.AddSingleton<IEngine, Engine>();
            serviceCollection.AddSingleton<ISession, Session>();

            serviceCollection.AddTransient<IReader, ConsoleReader>();
            serviceCollection.AddTransient<IWriter, ConsoleWriter>();
            serviceCollection.AddTransient<ICommandDispatcher, CommandDispatcher>();

            serviceCollection.AddTransient<IDbInitializeService, DbInitializeService>();
            serviceCollection.AddTransient<IEventService, EventService>();
            serviceCollection.AddTransient<IInvitationService, InvitationService>();
            serviceCollection.AddTransient<ITeamEventService, TeamEventService>();
            serviceCollection.AddTransient<ITeamService, TeamService>();
            serviceCollection.AddTransient<IUserService, UserService>();
            serviceCollection.AddTransient<IUserTeamService, UserTeamService>();

            var serviceProvider = serviceCollection.BuildServiceProvider();
            return serviceProvider;
        }
    }
}
