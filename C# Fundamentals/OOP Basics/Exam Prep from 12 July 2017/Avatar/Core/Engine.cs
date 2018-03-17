using System;
using System.Linq;
using System.Text;

public class Engine
{
    private const string END_COMMAND = "Quit";
    private readonly StringBuilder _sb;
    private readonly NationsBuilder _nationsBuilder;

    public Engine()
    {
        _sb = new StringBuilder();
        _nationsBuilder = new NationsBuilder();
    }

    public void Run()
    {
        string input;
        while ((input = Console.ReadLine()) != END_COMMAND)
        {
            ParseCommand(input);
        }

        _sb.AppendLine(_nationsBuilder.GetWarsRecord());

        Console.WriteLine(_sb.ToString().TrimEnd());
    }

    private void ParseCommand(string input)
    {
        var commandArgs = input.Split();
        var command = commandArgs[0];
        var arguments = commandArgs.Skip(1).ToList();
        switch (command)
        {
            case "Bender":
                _nationsBuilder.AssignBender(arguments);
                break;
            case "Monument":
                _nationsBuilder.AssignMonument(arguments);
                break;
            case "Status":
                _sb.AppendLine(_nationsBuilder.GetStatus(arguments.First()));
                break;
            case "War":
                _nationsBuilder.IssueWar(arguments.First());
                break;
        }
    }
}