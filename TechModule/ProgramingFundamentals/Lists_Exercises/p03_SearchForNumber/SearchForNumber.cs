using System;
using System.Collections.Generic;
using System.Linq;

namespace p03_SearchForNumber
{
    class SearchForNumber
    {
        static void Main()
        {
            List<int> numbers = Console.ReadLine()
                .Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();
            int[] tokens = Console.ReadLine()
                .Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
            int takeCount = tokens[0];
            int deleteCount = tokens[1];
            int searchedNum = tokens[2];

            numbers = numbers
                .Take(takeCount)
                .ToList();
            numbers.RemoveRange(0, deleteCount);
            Console.WriteLine(numbers.Contains(searchedNum) ? "YES!" : "NO!");

        }
    }
}
