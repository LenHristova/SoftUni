using System.Linq;

public class Engine
{
    private readonly DraftManager _draftManager;
    private bool _isRunning;

    public Engine()
    {
        _draftManager = new DraftManager();
        _isRunning = true;
    }

    public void Run()
    {
        while (_isRunning)
        {
            try
            {
                var input = InputReader.ReadLine();
                var resultFromTheCommand = ParseCommand(_draftManager, input);
                OutputWriter.WriteLine(resultFromTheCommand);
            }
            catch (InvalidCommandExeption commandExeption)
            {
                OutputWriter.WriteLine(commandExeption.Message);
            }
        }
    }

    private string ParseCommand(DraftManager draftManager, string input)
    {
        var comandArgs = input.Split().ToList();
        var command = comandArgs[0];
        var arguments = comandArgs.Skip(1).ToList();
        string outputMessage;

        switch (command)
        {
            case "RegisterHarvester":               
                outputMessage = draftManager.RegisterHarvester(arguments);
                break;
            case "RegisterProvider":
                outputMessage = draftManager.RegisterProvider(arguments);
                break;
            case "Day":
                outputMessage = draftManager.Day();
                break;
            case "Mode":
                outputMessage = draftManager.Mode(arguments);
                break;
            case "Check":
                 outputMessage = draftManager.Check(arguments);
                break;
            case "Shutdown":
                outputMessage = draftManager.ShutDown();
                _isRunning = false;
                break;
            default:
                throw new InvalidCommandExeption();
        }

        return outputMessage;
    }
}