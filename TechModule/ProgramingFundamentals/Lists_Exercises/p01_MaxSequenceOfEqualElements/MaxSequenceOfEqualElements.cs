using System;
using System.Collections.Generic;
using System.Linq;

namespace p01_MaxSequenceOfEqualElements
{
    class MaxSequenceOfEqualElements
    {
        static void Main()
        {
            List<int> numbers = Console.ReadLine()
                .Split(new[] { ' ' }, StringSplitOptions.None)
                .Select(int.Parse)
                .ToList();

            List<int> maxSequence = GetMax(numbers);

            Console.WriteLine(string.Join(" ", maxSequence));
        }
        // return longest sequence of equal elements
        private static List<int> GetMax(List<int> numbers)
        {
            List<int> maxSequence = new List<int>();
            List<int> currentSequence = new List<int>();
            for (int pos = 0; pos < numbers.Count - 1; pos++)
            {
                currentSequence.Add(numbers[pos]);
                if (numbers[pos] == numbers[pos + 1])
                {
                    if (pos != numbers.Count - 2)
                    {
                        continue;
                    }
                    currentSequence.Add(numbers[pos + 1]);
                }
                if (currentSequence.Count > maxSequence.Count)
                {
                    maxSequence.Clear();
                    maxSequence = currentSequence.Take(currentSequence.Count).ToList();
                }
                currentSequence.Clear();
            }
            return maxSequence;
        }
    }
}
