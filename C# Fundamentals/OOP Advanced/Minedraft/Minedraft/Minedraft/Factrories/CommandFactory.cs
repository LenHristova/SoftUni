using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public class CommandFactory : ICommandFactory
{
    private readonly IServiceProvider _serviceProvider;

    public CommandFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public ICommand GenerateCommand(IList<string> args)
    {
        var commandName = args[0] + nameof(Command);

        var commandType = Assembly
            .GetCallingAssembly()
            .GetTypes()
            .FirstOrDefault(t => t.Name == commandName);

        if (commandType == null)
        {
            throw new ArgumentException(string.Format(OutputMessages.NOT_FOUND, nameof(Command)));
        }

        if (!typeof(ICommand).IsAssignableFrom(commandType))
        {
            throw new InvalidOperationException(string.Format(OutputMessages.NOT_ASSIGNABLE_FROM, commandType, nameof(ICommand)));
        }

        var commandArgs = args.Skip(1).ToList();
        var ctorParams = commandType
            .GetConstructors()
            .FirstOrDefault()?
            .GetParameters()
            .Select(p => p.ParameterType == typeof(IList<string>) 
                ? commandArgs 
                : _serviceProvider.GetService(p.ParameterType))
            .ToArray();

        var command = (ICommand)Activator.CreateInstance(commandType, ctorParams);
        return command;
    }
}