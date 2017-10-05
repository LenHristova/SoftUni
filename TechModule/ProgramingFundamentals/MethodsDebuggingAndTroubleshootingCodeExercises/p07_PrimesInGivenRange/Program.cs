using System;
using System.Collections.Generic;

namespace p07_PrimesInGivenRange
{
    class Program
    {
        static void Main(string[] args)
        {
            long rangeFirstNum = long.Parse(Console.ReadLine());
            long rangeLastNum = long.Parse(Console.ReadLine());

            List<long> primeNums = new List<long>();
            AddPrimes(primeNums, rangeFirstNum, rangeLastNum);

            Console.WriteLine(string.Join(", ", primeNums));
        }

        //Add prime numbers in given range
        static void AddPrimes(List<long> primeNums, long rangeFirstNum, long rangeLastNum)
        {
            for (long checkedNum = rangeFirstNum; checkedNum <= rangeLastNum; checkedNum++)
            {
                if (IsPrime(checkedNum))
                {
                    primeNums.Add(checkedNum);
                }
            }
        }

        private static bool IsPrime(long number)
        {
            if (number < 2)
            {
                return false;
            }
            for (int i = 2; i <= Math.Sqrt(number); i++)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
