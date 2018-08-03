namespace PhotoShare.Client.Core
{
    using System;
    using System.Linq;
    using System.Reflection;

    using Contracts;

    public class CommandInterpreter : ICommandInterpreter
    {
        private readonly IServiceProvider serviceProvider;

        public CommandInterpreter(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public string Read(string[] commandParameters)
        {
            var commandName = commandParameters[0];

            var commandType = Assembly.GetExecutingAssembly()
                                        .GetTypes()
                                        .FirstOrDefault(t => 
                                                    typeof(ICommand).IsAssignableFrom(t) &&
                                                    t.Name == $"{commandName}Command");

            if (commandType == null)
            {
                throw new ArgumentException($"Command {commandName} not found!");
            }

            var commandArgs = commandType
                .GetConstructors()
                .First()
                .GetParameters()
                .Select(p => serviceProvider.GetService(p.ParameterType))
                .ToArray();

            var command = (ICommand)Activator.CreateInstance(commandType, commandArgs);

            var data = commandParameters
                .Skip(1)
                .ToArray();

            var result = command.Execute(data);
            return result;
        }
    }
}
