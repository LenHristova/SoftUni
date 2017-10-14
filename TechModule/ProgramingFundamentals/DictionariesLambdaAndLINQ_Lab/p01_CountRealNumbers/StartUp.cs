using System;
using System.Collections.Generic;
using System.Linq;

class StartUp
{
    static void Main()
    {
        double[] numbers = Console.ReadLine()
            .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(double.Parse)
            .ToArray();
        SortedDictionary<double, int> counts = GetDictionary(numbers);

        foreach (var count in counts)
        {
            Console.WriteLine($"{count.Key} -> {count.Value}");
        }
    }

    static SortedDictionary<double, int> GetDictionary(double[] numbers)
    {
        SortedDictionary<double, int> counts = new SortedDictionary<double, int>();

        foreach (double num in numbers)
        {
            if (!counts.ContainsKey(num))
            {
                counts[num] = 0;
            }
            counts[num]++;
        }

        return counts;
    }
}

