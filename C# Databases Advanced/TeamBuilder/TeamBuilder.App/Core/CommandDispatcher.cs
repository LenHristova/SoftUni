namespace TeamBuilder.App.Core
{
	using System;
	using System.Linq;
	using System.Reflection;
	using Contracts;

    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider serviceProvider;

        public CommandDispatcher(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public string Dispatch(string input)
        {
            var args = input
                .Split(new[] {' ', '\t'}, StringSplitOptions.RemoveEmptyEntries);

            var commandName = args.FirstOrDefault() ?? string.Empty;

            var commandType = Assembly.GetExecutingAssembly()
                .GetTypes()
                .FirstOrDefault(t => typeof(ICommand).IsAssignableFrom(t) &&
                                     t.Name == $"{commandName}Command");

            if (commandType == null)
            {
                throw new NotSupportedException($"Command {commandName} not supported!");
            }

            var commandParameters = commandType
                .GetConstructors()
                .First()
                .GetParameters()
                .Select(p => this.serviceProvider.GetService(p.ParameterType))
                .ToArray();

            var command = (ICommand)Activator.CreateInstance(commandType, commandParameters);

            var commandArgs = args
                .Skip(1)
                .ToArray();
            var result = command.Execute(commandArgs);

            return result;
        }
    }
}
