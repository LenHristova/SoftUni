using System;
using System.Collections.Generic;
using System.Linq;

namespace p01_RemoveNegativesAndReverseVariant2
{
    class RemoveNegativesAndReverseVariant2
    {
        static void Main()
        {
            List<int> numbers = Console.ReadLine().Split(' ').Select(int.Parse).ToList();
            numbers.RemoveAll(i => i < 0);
            numbers.Reverse();
            Console.WriteLine(numbers.Count != 0 ? string.Join(" ", numbers) : "empty");
        }
    }
}