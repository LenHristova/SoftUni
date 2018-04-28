using System;
using System.Linq;
using System.Reflection;

using P03_BarraksWars.Attributes;
using P03_BarraksWars.Contracts;

namespace P03_BarraksWars.Core
{
    public class CommandInterpreter : ICommandInterpreter
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandInterpreter(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IExecutable InterpretCommand(string[] data, string commandName)
        {
            var commandType = Assembly
                .GetCallingAssembly()
                .GetTypes()
                .FirstOrDefault(t => t.Name.ToLower() == commandName + "command");

            if (commandType == null)
            {
                throw new ArgumentException($"Invalid command type: {commandName}!");
            }

            if (!typeof(IExecutable).IsAssignableFrom(commandType))
            {
                throw new ArgumentException($"{commandType} is not a Command!");
            }

            var injectFields = commandType
                .GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                .Where(f => f.CustomAttributes.Any(ca => ca.AttributeType == typeof(InjectAttribute)));

            var injectArgs = injectFields
                .Select(f => _serviceProvider.GetService(f.FieldType))
                .ToArray();

            var constructorArgs = new object[] {data }.Concat(injectArgs).ToArray();
            var command = (IExecutable)Activator.CreateInstance(commandType, constructorArgs);

            return command;
        }
    }
}
