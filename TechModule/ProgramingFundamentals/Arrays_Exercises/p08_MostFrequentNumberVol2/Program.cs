using System;
using System.Linq;

class Program
{
    static void Main()
    {
        int mostFrequentNumber = Console.ReadLine()
            .Split(' ')
            .Select(int.Parse)
            .GroupBy(n => n)
            .OrderByDescending(n => n.Count())
            .First().Key;
        Console.WriteLine(mostFrequentNumber);
    }
}