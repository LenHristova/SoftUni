using System;
using System.Collections.Generic;

class StartUp
{
    static void Main()
    {
        var numbers = Console.ReadLine()
            .Split(new[] {' ', '\t', '\n'}, StringSplitOptions.RemoveEmptyEntries);

        var numbersCounts = new SortedDictionary<string, int>();

        foreach (var number in numbers)
        {
            if (!numbersCounts.ContainsKey(number))
            {
                numbersCounts[number] = 0;
            }

            numbersCounts[number]++;
        }

        foreach (var numberCount in numbersCounts)
        {
            Console.WriteLine($"{numberCount.Key} -> {numberCount.Value} times");
        }
    }
}