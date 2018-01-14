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
        Console.WriteLine($"Sum={numbers.Sum()}; Average={numbers.Average():F2}");

    }
}