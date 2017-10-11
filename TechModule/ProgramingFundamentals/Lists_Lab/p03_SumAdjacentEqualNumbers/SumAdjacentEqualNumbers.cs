using System;
using System.Collections.Generic;
using System.Linq;

namespace p03_SumAdjacentEqualNumbers
{
    class SumAdjacentEqualNumbers
    {
        static void Main()
        {
            List<decimal> numbers = Console.ReadLine()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(decimal.Parse)
                .ToList();

            for (int pos = 1; pos < numbers.Count; pos++)
            {
                if (numbers[pos] != numbers[pos - 1]) continue;
                numbers[pos] += numbers[pos - 1];
                numbers.RemoveAt(pos - 1);
                pos = 0;
            }
            Console.WriteLine(string.Join(" ", numbers));
        }
    }
}