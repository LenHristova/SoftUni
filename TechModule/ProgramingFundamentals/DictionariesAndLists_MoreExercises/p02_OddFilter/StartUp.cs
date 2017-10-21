using System;
using System.Linq;

namespace p02_OddFilter
{
    class StartUp
    {
        static void Main(string[] args)
        {
            int[] numbers = Console.ReadLine()
                .Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .Where(n => n % 2 == 0)
                .ToArray();
            int[] convertedNumbers = numbers
                .Select(n => n > numbers.Average() ? n + 1 : n - 1)
                .ToArray();
            Console.WriteLine(string.Join(" ", convertedNumbers));
        }
    }
}