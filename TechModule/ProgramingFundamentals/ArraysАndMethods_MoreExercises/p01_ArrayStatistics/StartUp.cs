using System;
using System.Linq;

namespace p01_ArrayStatistics
{
    class StartUp
    {
        static void Main()
        {
            int[] numbers = Console.ReadLine()
                .Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
            Console.WriteLine($"Min = {numbers.Min()}\r\n" +
                              $"Max = {numbers.Max()}\r\n" +
                              $"Sum = {numbers.Sum()}\r\n" +
                              $"Average = {numbers.Average()}");
        }
    }
}