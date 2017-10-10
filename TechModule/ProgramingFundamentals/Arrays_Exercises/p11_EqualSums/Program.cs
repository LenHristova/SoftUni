using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace p11_EqualSums
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] nums = Console.ReadLine()
                .Split(' ')
                .Select(int.Parse)
                .ToArray();

            PRint(nums);
        }

        private static void PRint(int[] nums)
        {
            for (int i = 0; i < nums.Length; i++)
            {
                if (nums.Take(i).Sum() == nums.Skip(i + 1).Sum())
                {
                    Console.WriteLine(i);
                    break;
                }
            }
        }
    }
}
