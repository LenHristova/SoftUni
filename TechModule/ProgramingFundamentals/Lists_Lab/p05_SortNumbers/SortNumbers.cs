using System;
using System.Collections.Generic;
using System.Linq;

namespace p05_SortNumbers
{
    class SortNumbers
    {
        static void Main()
        {
            List<decimal> numbers = Console.ReadLine()
                .Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries)
                .Select(decimal.Parse)
                .ToList();
            numbers.Sort();

            Console.WriteLine(string.Join(" <= ", numbers));
        }
    }
}
