using System;
using System.Linq;

namespace p06_SumReversedNumbers
{
    class SumReversedNumbers
    {
        static void Main()
        {
            int sum = Console.ReadLine()
                .Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries)
                .Select(num => string.Join("", num.ToCharArray().Reverse()))
                .Select(int.Parse)
                .Sum();

            Console.WriteLine(sum);
        }
    }
}