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

        Console.WriteLine(GetElementIfExist(nums));
    }

    //search element that the sum of the elements on its left is equal to the sum of the elements on its right
    static string GetElementIfExist(int[] nums)
    {
        for (int pos = 0; pos < nums.Length; pos++)
        {
            if (nums.Take(pos).Sum() == nums.Skip(pos + 1).Sum())
            {
                return pos.ToString();
            }
        }
        return "no";
    }
}