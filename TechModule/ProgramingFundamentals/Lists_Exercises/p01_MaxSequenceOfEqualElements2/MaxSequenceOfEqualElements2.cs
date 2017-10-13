using System;
using System.Linq;

namespace p01_MaxSequenceOfEqualElements2
{
    class MaxSequenceOfEqualElements2
    {
        static void Main()
        {
            int[] numbers = Console.ReadLine()
                .Split(new[] { ' ' }, StringSplitOptions.None)
                .Select(int.Parse)
                .ToArray();

           int[] maxSubsequence = GetMax(numbers);

            Console.WriteLine(string.Join(" ", maxSubsequence));
        }
        // return longest sequence of equal elements
        private static int[] GetMax(int[] numbers)
        {  
            int counter = 1;
            int maxCount = 0;
            int num = numbers[0];
            for (int pos = 1; pos < numbers.Length; pos++)
            {
                if (numbers[pos] == numbers[pos - 1])
                {
                    counter++;
                    if (pos != numbers.Length - 1)
                    {
                        continue;
                    }
                }
                if (counter > maxCount)
                {
                    maxCount = counter;
                    num = numbers[pos-1];
                }
                counter = 1;
            }
            return new int[maxCount].Select(n => num).ToArray();
        }
    }
}
