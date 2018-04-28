using System;
using System.Linq;

public class Engine
{
    private readonly ICommandInterpreter _commandInterpreter;
    private readonly IReader _reader;

    public Engine(ICommandInterpreter commandInterpreter, IReader reader)
    {
        _commandInterpreter = commandInterpreter;
        _reader = reader;
    }

    public void Run()
    {
        string input;
        while ((input = _reader.ReadLine()) != "END")
        {
            var commandArgs = input.Split(";");
            var commandType = commandArgs[0];
            var data = commandArgs.Skip(1).ToArray();
            var command = _commandInterpreter.InterpretCommand(commandType, data);
            command.Execute();
        }
    }
}