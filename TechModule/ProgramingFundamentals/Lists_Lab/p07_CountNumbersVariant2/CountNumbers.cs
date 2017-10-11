using System;
using System.Collections.Generic;
using System.Linq;

namespace p07_CountNumbersVariant2
{
    class CountNumbers
    {
        static void Main()
        {
            List<int> numbers = Console.ReadLine()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();

            numbers.Sort();
            int[] uniqueNumbers = numbers.Distinct().ToArray();

            foreach (var uniqueNumber in uniqueNumbers)
            {
                Console.WriteLine($"{uniqueNumber} -> {numbers.Count(n => n == uniqueNumber)}");
            }
        }
    }
}
