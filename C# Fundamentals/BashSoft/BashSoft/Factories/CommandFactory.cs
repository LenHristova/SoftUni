using System;
using System.Linq;
using System.Reflection;

using BashSoft.Attributes;
using BashSoft.Contracts;

namespace BashSoft.Factories
{
    public class CommandFactory : ICommandFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IExecutable CreateCommand(string commandName)
        {
            var commandType = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .FirstOrDefault(type => type.GetCustomAttributes(typeof(AliasAttribute))
                                            .Where(attribute => attribute.Equals(commandName.ToLower()))
                                            .ToArray().Length > 0);
            if (commandType == null)
            {
                throw new ArgumentException("Invalid command!");
            }

            if (!typeof(IExecutable).IsAssignableFrom(commandType))
            {
                throw new InvalidOperationException("$Command \"{commandName}\" is not IExecutable");
            }

            var ctorParams = commandType
                .GetConstructors()
                .First()
                .GetParameters()
                .Select(p => _serviceProvider.GetService(p.ParameterType))
                .ToArray();

            var command = (IExecutable)Activator.CreateInstance(commandType, ctorParams);
            return command;
        }
    }
}