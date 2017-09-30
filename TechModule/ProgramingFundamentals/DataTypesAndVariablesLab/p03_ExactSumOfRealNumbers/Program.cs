using System;

namespace p03_ExactSumOfRealNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            int numsCount = int.Parse(Console.ReadLine());
            decimal exactSum = 0M;
            for (int currentNum = 0; currentNum < numsCount; currentNum++)
            {
                exactSum += decimal.Parse(Console.ReadLine());
            }
            Console.WriteLine(exactSum);
        }
    }
}
