namespace Employees.App.Core
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Commands.Contracts;
    using Contracts;

    internal class CommandParser : ICommandParser
    {
        private readonly IServiceProvider serviceProvider;

        public CommandParser(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public ICommand ParseCommand(string commandName)
        {
            var commandType = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .SingleOrDefault(t => typeof(ICommand).IsAssignableFrom(t) && t.Name == $"{commandName}Command");

            if (commandType == null)
            {
                throw new InvalidOperationException("Invalid command!");
            }

            var ctorParams = commandType.GetConstructors().First()
                .GetParameters()
                .Select(p => serviceProvider.GetService(p.ParameterType))
                .ToArray();

            var command = Activator.CreateInstance(commandType, ctorParams);

            return (ICommand) command;
        }
    }
}