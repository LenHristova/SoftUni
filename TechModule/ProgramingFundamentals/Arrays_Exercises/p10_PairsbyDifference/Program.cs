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
        int diff = int.Parse(Console.ReadLine());

        int pairsCount = CountPairs(nums, diff);
        Console.WriteLine(pairsCount);
    }

    static int CountPairs(int[] nums, int diff)
    {
        int counter = 0;

        foreach (int currentNum in nums)
        {
            foreach (int checkedNum in nums)
            {
                if (currentNum + diff == checkedNum)
                {
                    counter++;
                }
            }
        }
        return counter;
    }
}

