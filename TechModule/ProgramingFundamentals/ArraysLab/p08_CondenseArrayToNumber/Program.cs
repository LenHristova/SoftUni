using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        List<int> numbers = Console.ReadLine()
            .Split(' ')
            .Select(int.Parse)
            .ToList();

        while (numbers.Count > 1)
        {
            for (int pos = 0; pos < numbers.Count - 1; pos++)
            {
                numbers[pos] += numbers[pos + 1];
            }
            numbers.RemoveAt(numbers.Count - 1);
        }

        Console.WriteLine(numbers[0]);
    }
}

