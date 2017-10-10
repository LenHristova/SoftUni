using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        int[] nums = Console.ReadLine()
            .Split(' ')
            .Select(int.Parse)
            .ToArray();

        List<int> maxSequenceOfIncElements = FindMaxSequenceOfIncElements(nums);
        Console.WriteLine(string.Join(" ", maxSequenceOfIncElements));
    }

    private static List<int> FindMaxSequenceOfIncElements(int[] nums)
    {
        List<int> maxSequenceOfIncElements = new List<int>();

        for (int pos = 0; pos < nums.Length - 1; pos++)
        {
            List<int> currentSequence = new List<int>();
            currentSequence.Add(nums[pos]);
            while (pos < nums.Length - 1 && nums[pos] < nums[pos + 1])
            {
                currentSequence.Add(nums[pos + 1]);
                pos++;
            }
            if (currentSequence.Count > maxSequenceOfIncElements.Count)
            {
                maxSequenceOfIncElements.Clear();
                maxSequenceOfIncElements.AddRange(currentSequence);
            }
            currentSequence.Clear();
        }
        return maxSequenceOfIncElements;
    }
}

