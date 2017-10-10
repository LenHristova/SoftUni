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
        int[] middleNums = GetMiddleNums(nums);
        int[] sideNums = GetSideNums(nums);
        int[] sumNums = GetSumedNums(middleNums, sideNums);
        Console.WriteLine(string.Join(" ", sumNums));
    }

    private static int[] GetSumedNums(int[] middleNums, int[] sideNums)
    {
        int[] sumNums = new int[middleNums.Length];
        for (int pos = 0; pos < middleNums.Length; pos++)
        {
            sumNums[pos] = middleNums[pos] + sideNums[pos];
        }
        return sumNums;
    }

    static int[] GetSideNums(int[] nums)
    {
        int[] sideNums = new int[nums.Length / 2];

        for (int pos = 0; pos < sideNums.Length / 2; pos++)
        {
            sideNums[pos] = nums[sideNums.Length / 2 - 1 - pos];
            sideNums[pos + sideNums.Length / 2] = nums[nums.Length - 1 - pos];
        }
        return sideNums;
    }

    static int[] GetMiddleNums(int[] nums)
    {
        int[] middleNums = new int[nums.Length / 2];
        for (int pos = 0; pos < middleNums.Length; pos++)
        {
            middleNums[pos] = nums[nums.Length / 4 + pos];
        }
        return middleNums;
    }
}
