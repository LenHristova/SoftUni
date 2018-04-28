using System;
using System.Linq;
using System.Reflection;

using DungeonsAndCodeWizards.Contracts;

namespace DungeonsAndCodeWizards.Factories
{
    public class CommandFactory : ICommandFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ICommand CreateCommand(string commandName)
        {
            var commandType = Assembly
                .GetCallingAssembly()
                .GetTypes()
                .FirstOrDefault(t => t.Name == commandName);

            if (commandType == null)
            {
                throw new ArgumentException("Command not found");
            }

            if (!typeof(ICommand).IsAssignableFrom(commandType))
            {
                throw new InvalidOperationException($"{commandName} is not ICommand");
            }

            var ctorParams = commandType
                .GetConstructors()
                .FirstOrDefault()?
                .GetParameters()
                .Select(p => _serviceProvider.GetService(p.ParameterType))
                .ToArray();

            var command = (ICommand)Activator.CreateInstance(commandType, ctorParams);
            return command;
        }
    }
}