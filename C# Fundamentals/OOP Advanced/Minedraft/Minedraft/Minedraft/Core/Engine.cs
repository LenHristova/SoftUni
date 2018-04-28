using System.Linq;

public class Engine
{
    private const string END_COMMAND = "Shutdown";

    private readonly IReader _reader;
    private readonly IWriter _writer;
    private readonly ICommandInterpreter _commandInterpreter;

    public Engine(ICommandInterpreter commandInterpreter, IReader reader, IWriter writer)
    {
        _commandInterpreter = commandInterpreter;
        _reader = reader;
        _writer = writer;
    }

    public void Run()
    {
        while (true)
        {
            var data = _reader.ReadLine().Split().ToList();
            _writer.WriteLine(_commandInterpreter.ProcessCommand(data));

            if (data[0] == END_COMMAND)
            {
                break;
            }
        }
    }
}
