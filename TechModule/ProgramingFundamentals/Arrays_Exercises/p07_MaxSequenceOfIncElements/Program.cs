using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Program
{
    static void Main()
    {
        int[] nums = Console.ReadLine()
            .Split(' ')
            .Select(int.Parse)
            .ToArray();

        int[] maxSequenceOfIncElements = FindMaxSequenceOfIncElements(nums);
        Console.WriteLine(string.Join(" ", maxSequenceOfIncElements));
    }

    static int[] FindMaxSequenceOfIncElements(int[] nums)
    {
        int counter = 1;
        int max = 0;
        int index = 0;
        int lastPos = nums.Length;
        for (int pos = 1; pos < lastPos; pos++)
        {

            if (nums[pos - 1] < nums[pos])
            {
                counter++;
                if (pos != lastPos -1)
                {
                    continue;
                }
                pos++;
            }
            if (counter > max)
            {
                max = counter;
                index = pos - counter;
            }
            counter = 1;
        }
        return GetMaxSequenceOfIncElements(nums, max, index);

    }

    private static int[] GetMaxSequenceOfIncElements(int[] nums, int max, int index)
    {
        int[] maxSequenceOfIncElements = new int[max];

        for (int i = 0; i < max; i++)
        {
            maxSequenceOfIncElements[i] = nums[index + i];
        }

        return maxSequenceOfIncElements;
    }
}

