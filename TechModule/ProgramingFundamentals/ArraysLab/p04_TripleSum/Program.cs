using System;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        int[] nums = Console.ReadLine()
            .Split(' ')
            .Select(int.Parse)
            .ToArray();

        bool hasTriplesOfElements = false;
        for (int i = 0; i < nums.Length; i++)
        {
            for (int j = i + 1; j < nums.Length; j++)
            {
                for (int k = 0; k < nums.Length; k++)
                {
                    if (nums[i] + nums[j] == nums[k])
                    {
                        Console.WriteLine($"{nums[i]} + {nums[j]} == {nums[k]}");
                        hasTriplesOfElements = true;
                        break;
                    }
                }
            }
        }
        if (!hasTriplesOfElements)
        {
            Console.WriteLine("No");
        }
    }
}