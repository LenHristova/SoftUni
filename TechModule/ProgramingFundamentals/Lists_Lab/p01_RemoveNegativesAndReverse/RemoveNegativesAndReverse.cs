using System;
using System.Collections.Generic;
using System.Linq;

namespace p01_RemoveNegativesAndReverse
{
    public class RemoveNegativesAndReverse
    {
        static void Main()
        {
            List<int> reversedPositiveNumbers = Console.ReadLine()
                .Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries)
                .Where(n => !n.Contains('-'))
                .Select(int.Parse)
                .Reverse()
                .ToList();

            Console.WriteLine(reversedPositiveNumbers.Count == 0 ? 
                "empty" : string.Join(" ", reversedPositiveNumbers));
        }
    }
}