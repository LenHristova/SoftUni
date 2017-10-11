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

        int[] sumNums = GetSumedNums(nums);
        Console.WriteLine(string.Join(" ", sumNums));
    }

    static int[] GetSumedNums(int[] nums)
    {
        int quarterNumsLenght = nums.Length / 4;
        int halfNumsLenght = nums.Length / 2;
        int[] middleNums = GetMiddle(nums, quarterNumsLenght, halfNumsLenght);
        int[] sideNums = GetSides(nums, quarterNumsLenght, halfNumsLenght);
        return SumNums(middleNums, sideNums); ;
    }

    static int[] SumNums(int[] middleNums, int[] sideNums)
    {
        return new int[middleNums.Length]
        .Select((num, index) => middleNums[index] + sideNums[index])
        .ToArray();
    }

    static int[] GetSides(int[] nums, int quarterNumsLenght, int halfNumsLenght)
    {
        int[] leftSideNums = nums
            .Take(quarterNumsLenght)
            .ToArray();
        Array.Reverse(leftSideNums);

        int[] rightSideNums = nums
            .Skip(quarterNumsLenght + halfNumsLenght)
            .Take(quarterNumsLenght)
            .ToArray();
        Array.Reverse(rightSideNums);

        return leftSideNums.Concat(rightSideNums).ToArray();
    }

    static int[] GetMiddle(int[] nums, int quarterNumsLenght, int halfNumsLenght)
    {
        return nums
            .Skip(quarterNumsLenght)
            .Take(halfNumsLenght)
            .ToArray();
    }
}

