using System;
using System.Linq;
using System.Reflection;

using Forum.App.Commands;

namespace Forum.App.Factories
{
	using Contracts;

	public class CommandFactory : ICommandFactory
	{
	    private readonly IServiceProvider serviceProvider;

	    public CommandFactory(IServiceProvider serviceProvider)
	    {
	        this.serviceProvider = serviceProvider;
	    }

	    public ICommand CreateCommand(string commandName)
	    {
	        Type commandType = Assembly
	            .GetExecutingAssembly()
	            .GetTypes()
	            .FirstOrDefault(t => t.Name == commandName + "Command");

	        if (commandType == null)
	        {
	            throw new InvalidOperationException("Command not found!");
	        }

	        if (!typeof(ICommand).IsAssignableFrom(commandType))
	        {
	            throw new InvalidOperationException($"{commandType} is not a command!");
            }

	        var args = commandType
	            .GetConstructors()
	            .First()
	            .GetParameters()
	            .Select(p => serviceProvider.GetService(p.ParameterType))
	            .ToArray();

            ICommand command = (ICommand)Activator.CreateInstance(commandType, args);

	        return command;
	    }
	}
}
