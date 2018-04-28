using System;
using System.Linq;
using System.Reflection;

public class CommandInterpreter : ICommandInterpreter
{
    private readonly IServiceProvider _serviceProvider;

    public CommandInterpreter(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IExecutable InterpretCommand(string commandType, string[] data)
    {
        var type = Assembly
            .GetExecutingAssembly()
            .GetType(commandType + "Command");

        if (type == null)
        {
            throw new ArgumentException($"Invalid Command type: {commandType}");
        }

        if (!typeof(IExecutable).IsAssignableFrom(type))
        {
            throw new ArgumentException($"{commandType} is not a Command!");
        }

        var parametersFromServiceProvider = type
            .GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
            .Where(f => f.FieldType.IsInterface)
            .Select(f => _serviceProvider.GetService(f.FieldType));

        var parameters = new object[] {data}.Concat(parametersFromServiceProvider).ToArray();
        var command = (IExecutable)Activator.CreateInstance(type, parameters);
        return command;
    }
}