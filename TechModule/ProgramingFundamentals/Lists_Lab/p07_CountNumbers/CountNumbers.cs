using System;
using System.Collections.Generic;
using System.Linq;

namespace p07_CountNumbers
{
    class CountNumbers
    {
        static void Main()
        {
            List<int> numbers = Console.ReadLine()
                .Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();

            numbers.Sort();
            int[] uniqueNumbers = numbers.Distinct().ToArray();
            int[][] uniqueNumbersCount = new int[uniqueNumbers.Length][];

            for (int pos = 0; pos < uniqueNumbers.Length; pos++)
            {
                uniqueNumbersCount[pos] = new int[2];
                uniqueNumbersCount[pos][0] = uniqueNumbers[pos];
                uniqueNumbersCount[pos][1] = numbers.Count(n => n == uniqueNumbers[pos]);
            }
            Console.WriteLine(string.Join(Environment.NewLine, 
                uniqueNumbersCount.Select(arr => string.Join(" -> ", arr))));
        }
    }
}
