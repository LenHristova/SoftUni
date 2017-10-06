using System;

class Program
{
    static void Main(string[] args)
    {
        int numsCount = int.Parse(Console.ReadLine());
        int[] nums = new int[numsCount];

        for (int pos = numsCount - 1; pos >= 0; pos--)
        {
            nums[pos] = int.Parse(Console.ReadLine());
        }

        Console.WriteLine(string.Join(" ", nums));
    }
}

