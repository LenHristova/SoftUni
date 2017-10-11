using System;
using System.Collections.Generic;
using System.Linq;

namespace p06_SquareNumbers
{
    class SquareNumbers
    {
        static void Main()
        {
            List<int> numbers = Console.ReadLine()
                .Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();

            List<int> squareNumbers = numbers
                .Where(n => Math.Sqrt(n) == (int) Math.Sqrt(n))
                .OrderByDescending(n => n)
                .ToList();
            Console.WriteLine(string.Join(" ", squareNumbers));
        }
    }
}