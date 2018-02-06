using System;
using System.Collections.Generic;
using System.Linq;

class StartUp
{
    static void Main()
    {
        var gestsList = Console.ReadLine()
            .Split(new[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries)
            .ToList();

        var filters = new List<string>();
        string input;
        while ((input = Console.ReadLine()) != "Print")
        {
            var commandArgs = input;

            var command = commandArgs.Substring(0, commandArgs.IndexOf(';'));
            var filter = commandArgs.Substring(commandArgs.IndexOf(';'));
            switch (command)
            {
                case "Add filter":
                    filters.Add(filter);
                    break;
                case "Remove filter":
                    filters.Remove(filter);
                    break;
            }
        }

        foreach (var filter in filters)
        {

            gestsList = gestsList.Where(Filter(filter)).ToList();
        }

        Console.WriteLine(string.Join(" ", gestsList));
    }

    static Func<string, bool> Filter(string filter)
    {
        var filterParams = filter
            .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

        switch (filterParams[0])
        {
            case "Starts with":
                return gest => !gest.StartsWith(filterParams[1]);
            case "Ends with":
                return gest => !gest.EndsWith(filterParams[1]);
            case "Length":
                return gest => gest.Length != int.Parse(filterParams[1]);
            case "Contains":
                return gest => !gest.Contains(filterParams[1]);
            default:
                throw new NotSupportedException();
        }
    }
}