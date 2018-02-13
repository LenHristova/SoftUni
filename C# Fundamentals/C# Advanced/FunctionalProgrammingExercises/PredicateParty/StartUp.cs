using System;
using System.Collections.Generic;
using System.Linq;

class StartUp
{
    static void Main()
    {
        var gestsList = Console.ReadLine()
            .Split(new[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(p => new List<string>() { p })
            .ToList();

        string input;
        while ((input = Console.ReadLine()) != "Party!")
        {
            var commandArgs = input
                .Split(new[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            switch (commandArgs[0])
            {
                case "Remove":
                    gestsList = gestsList.Where(Remove(commandArgs)).ToList();
                    break;
                case "Double":
                    gestsList = gestsList.Select(Double(commandArgs)).ToList();
                    break;
            }

        }


        if (gestsList.Count > 0)
        {
            var entireGestsList = gestsList.Select(p => string.Join(", ", p));
            Console.WriteLine($"{string.Join(", ", entireGestsList)} are going to the party!");
        }
        else
        {
            Console.WriteLine("Nobody is going to the party!");
        }
    }

    static Func<List<string>, bool> Remove(string[] commandArgs)
    {
        switch (commandArgs[1])
        {
            case "StartsWith":
                return p => !p[0].StartsWith(commandArgs[2]);
            case "EndsWith":
                return p => !p[0].EndsWith(commandArgs[2]);
            case "Length":
                return p => p[0].Length != int.Parse(commandArgs[2]);
            default:
                throw new NotSupportedException();
        }
    }

    static Func<List<string>, List<string>> Double(string[] commandArgs)
    {
        switch (commandArgs[1])
        {
            case "StartsWith":
                return p =>
                {
                    if (p[0].StartsWith(commandArgs[2]))
                    {
                        p.AddRange(p);
                    }
                    return p;
                };
            case "EndsWith":
                return p =>
                {
                    if (p[0].EndsWith(commandArgs[2]))
                    {
                        p.AddRange(p);
                    }
                    return p;
                };
            case "Length":
                return p =>
                {
                    if (p[0].Length == int.Parse(commandArgs[2]))
                    {
                        p.AddRange(p);
                    }
                    return p;
                };
            default:
                throw new NotSupportedException();
        }
    }
}