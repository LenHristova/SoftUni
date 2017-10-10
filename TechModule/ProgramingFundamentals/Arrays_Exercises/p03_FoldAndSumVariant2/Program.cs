using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        List<int> nums = Console.ReadLine()
            .Split(' ')
            .Select(int.Parse)
            .ToList();

        List<int> sumNums = GetSumedNums(nums);
        Console.WriteLine(string.Join(" ", sumNums));
    }

    static List<int> GetSumedNums(List<int> nums)
    {
        int quarterNumsLenght = nums.Count / 4;
        int halfNumsLenght = nums.Count / 2;
        List<int> middleNums = GetMiddle(nums, quarterNumsLenght, halfNumsLenght);
        List<int> sideNums = GetSides(nums, quarterNumsLenght, halfNumsLenght);
        return SumNums(middleNums, sideNums); ;
    }

    static List<int> SumNums(List<int> middleNums, List<int> sideNums)
    {
        return new List<int>(middleNums)
            .Select((num, index) => num + sideNums[index])
            .ToList();
    }

    static List<int> GetSides(List<int> nums, int quarterNumsLenght, int halfNumsLenght)
    {
        return nums
            .Take(quarterNumsLenght)
            .Reverse()
            .Concat(nums
            .Skip(quarterNumsLenght + halfNumsLenght)
            .Take(quarterNumsLenght)
            .Reverse())
            .ToList();
    }

    static List<int> GetMiddle(List<int> nums, int quarterNumsLenght, int halfNumsLenght)
    {
        return nums
            .Skip(quarterNumsLenght)
            .Take(halfNumsLenght)
            .ToList();
    }
}

