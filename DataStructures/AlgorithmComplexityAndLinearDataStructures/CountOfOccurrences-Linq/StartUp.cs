using System;
using System.Linq;

class StartUp
{
    static void Main()
    {
        var numbersCounts = Console.ReadLine()
            .Split(new[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries)
            .GroupBy(n => n)
            .ToDictionary(k => k.Key, v => v.Count());

        foreach (var numberCount in numbersCounts.OrderBy(kvp => kvp.Key))
        {
            Console.WriteLine($"{numberCount.Key} -> {numberCount.Value} times");
        }
    }
}