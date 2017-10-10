using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Program
{
    static void Main(string[] args)
    {
        int[] nums = Console.ReadLine()
            .Split(' ')
            .Select(int.Parse)
            .ToArray();

        List<int> longestSequenceOfEqualElements = FindLongestSequenceOfEqualElements(nums);

        Console.WriteLine(string.Join(" ", longestSequenceOfEqualElements));
    }

    static List<int> FindLongestSequenceOfEqualElements(int[] nums)
    {
        List<List<int>> allSequences = new List<List<int>>();
        int counter = 1;
        for (int pos = 0; pos < nums.Length - 1; pos++)
        {
            if (nums[pos] == nums[pos + 1])
            {
                counter++;
                if (pos == nums.Length - 2)
                {
                    allSequences.Add(new int[counter]
                        .Select(n => nums[pos]).ToList());
                    break;
                }
            }
            else
            {
                allSequences.Add(new int[counter]
                    .Select(n => nums[pos]).ToList());
                counter = 1;
            }
        }
        return allSequences.OrderByDescending(l => l.Count).First();
    }
}

