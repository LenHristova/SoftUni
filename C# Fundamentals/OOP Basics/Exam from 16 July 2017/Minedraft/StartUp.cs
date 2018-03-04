using System;
using System.Linq;
using System.Text;

public class StartUp
{
    static void Main()
    {
        var draftManager = new DraftManager();
        var sb = new StringBuilder();

        while (true)
        {
            try
            {
                var input = Console.ReadLine();
                var resultFromTheCommand = ParseCommand(draftManager, input);
                sb.AppendLine(resultFromTheCommand);

                if (input == "Shutdown")
                    break;
            }
            catch (ArgumentException e)
            {
                sb.AppendLine(e.Message);
            }
        }

        Console.WriteLine(sb.ToString().TrimEnd());
    }

    private static string ParseCommand(DraftManager draftManager, string input)
    {
        var comandArgs = input?.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var command = comandArgs[0];
        var arguments = comandArgs.Skip(1).ToList();
        switch (command)
        {
            case "RegisterHarvester":
                return draftManager.RegisterHarvester(arguments);

            case "RegisterProvider":
                return draftManager.RegisterProvider(arguments);

            case "Day":
                return draftManager.Day();

            case "Mode":
                return draftManager.Mode(arguments);

            case "Check":
                return draftManager.Check(arguments);

            case "Shutdown":
                return draftManager.ShutDown();

            default:
                throw new NotSupportedException();
        }
    }
}