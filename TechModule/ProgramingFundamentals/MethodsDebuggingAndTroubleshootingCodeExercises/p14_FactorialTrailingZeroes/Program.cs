using System;
using System.Numerics;

namespace p14_FactorialTrailingZeroes
{
    class Program
    {
        static void Main(string[] args)
        {
            int num = int.Parse(Console.ReadLine());
            BigInteger factorial = GetFactorial(num);
            int trailingZerosCount = CountTrailingZeros(factorial);
            Console.WriteLine(trailingZerosCount);
        }

        private static int CountTrailingZeros(BigInteger factorial)
        {
            int trailingZerosCount = 0;
            while (true)
            {
                if (factorial % 10 == 0)
                {
                    trailingZerosCount++;
                }
                else
                {
                    break;
                }
                factorial /= 10;
            }
            return trailingZerosCount;
        }

        private static BigInteger GetFactorial(int num)
        {
            BigInteger factorial = 1;
            for (int i = 1; i <= num; i++)
            {
                factorial *= i;
            }
            return factorial;
        }
    }
}
