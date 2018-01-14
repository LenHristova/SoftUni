using System;
using System.Linq;

class StartUp
{
    static void Main()
    {
        var words = Console.ReadLine()
            .Split(new[] {' ', '\t', '\n'}, StringSplitOptions.RemoveEmptyEntries)
            .ToList();
        words.Sort();
        Console.WriteLine(string.Join(" ", words));
    }
}