namespace P01_BillsPaymentSystem.Factories
{
	using System;
	using System.Linq;
	using System.Reflection;
	using Contracts;
	using Contracts.Factories;

    public class CommandFactory : ICommandFactory
    {
        private readonly IServiceProvider serviceProvider;

        public CommandFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public ICommand CreateCommand(string commandName)
        {
            var commandType = Assembly.GetExecutingAssembly()
                .GetTypes()
                .FirstOrDefault(t => typeof(ICommand).IsAssignableFrom(t) && t.Name == commandName && !t.IsInterface && !t.IsAbstract);

            if (commandType == null)
            {
                throw new InvalidOperationException("Command not found!");
            }

            var args = commandType
                .GetConstructors()
                .First()
                .GetParameters()
                .Select(p => serviceProvider.GetService(p.ParameterType))
                .ToArray();

            var command = Activator.CreateInstance(commandType, args);

            return (ICommand)command;
        }
    }
}
