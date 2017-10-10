using System;
using System.Linq;

class Program
{
    static void Main()
    {
        int[] nums = Console.ReadLine()
            .Split(' ')
            .Select(int.Parse)
            .ToArray();

        int[] longestSequenceOfEqualElements = FindLongestSequenceOfEqualElements(nums);

        Console.WriteLine(string.Join(" ", longestSequenceOfEqualElements));
    }

    static int[] FindLongestSequenceOfEqualElements(int[] nums)
    {
        int counter = 0;
        int max = -1;
        int element = nums[0];
        int lastPos = nums.Length;
        for (int pos = 0; pos < lastPos; pos++)
        {
            if (pos != lastPos - 1)
            {
                if (nums[pos] == nums[pos + 1])
                {
                    counter++;
                }
                else
                {
                    if (counter > max)
                    {
                        max = counter;
                        element = nums[pos];
                    }
                    counter = 0;
                }
            }
            else
            {
                if (counter > max)
                {
                    max = counter;
                    element = nums[pos];
                }
            }
        }
        return new int[max + 1].Select(n => element).ToArray();
    }
}

