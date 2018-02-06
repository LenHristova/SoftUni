using System;
using System.Collections.Generic;
using System.Linq;

class StartUp
{
    static void Main()
    {
        var numbers = Console.ReadLine()
            .Split(new[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToArray();

        var filters = new HashSet<string>();
        string commandArgs;
        while ((commandArgs = Console.ReadLine()) != "Forge")
        {
            ParseCommand(commandArgs, filters); 
        }

        var numbersForExclide = new List<int>();

        foreach (var filter in filters)
        {
            var filtered = numbers.Where(Filter(filter, numbers));
            numbersForExclide.AddRange(filtered);
        }

        numbers = numbers.Where(Exclude(numbersForExclide)).ToArray();

        Console.WriteLine(string.Join(" ", numbers));
    }

    private static void ParseCommand(string commandArgs, HashSet<string> filters)
    {
        var command = commandArgs.Substring(0, commandArgs.IndexOf(';'));
        var filter = commandArgs.Substring(commandArgs.IndexOf(';'));
        switch (command)
        {
            case "Exclude":
                filters.Add(filter);
                break;
            case "Reverse":
                filters.Remove(filter);
                break;
        }
    }

    static Func<int, int, bool> Filter(string filter, int[] arr)
    {
        var filterParams = filter
            .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

        var comparedNumber = int.Parse(filterParams[1]);

        return (num, index) =>
        {
            var leftNum = index > 0 ? arr[index - 1] : 0;
            var rightNum = index < arr.Length - 1 ? arr[index + 1] : 0;

            switch (filterParams[0])
            {
                case "Sum Left":
                    return num + leftNum == comparedNumber;

                case "Sum Right":
                    return num + rightNum == comparedNumber;

                case "Sum Left Right":
                    return leftNum + num + rightNum == comparedNumber;
                default:
                    throw new NotSupportedException();
            }
        };
    }

    static Func<int, bool> Exclude(List<int> numbersForExclide)
    {
        return num => !numbersForExclide.Contains(num);
    }
}