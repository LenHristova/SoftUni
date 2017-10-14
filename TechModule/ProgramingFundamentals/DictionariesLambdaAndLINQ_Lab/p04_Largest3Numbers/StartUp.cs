using System;
using System.Linq;

class StartUp
{
    static void Main()
    {
        double[] nums = Console.ReadLine()
            .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(double.Parse)
            .OrderByDescending(s => s)
            .Take(3)
            .ToArray();

        Console.WriteLine(string.Join(" ", nums));
    }
}

