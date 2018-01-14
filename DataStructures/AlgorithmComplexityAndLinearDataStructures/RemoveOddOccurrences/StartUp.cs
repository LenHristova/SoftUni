using System;
using System.Linq;

class StartUp
{
    static void Main()
    {
        var numbers = Console.ReadLine()
            .Split(new[] {' ', '\t', '\n'}, StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToList();

        var numberCount = numbers.GroupBy(n => n)
            .ToDictionary(k => k.Key, v => v.Count());

        foreach (var number in numbers)
        {
            if (numberCount[number] % 2 == 0)
            {
                Console.Write(number + " ");
            }
        }

        Console.WriteLine();
    }
}