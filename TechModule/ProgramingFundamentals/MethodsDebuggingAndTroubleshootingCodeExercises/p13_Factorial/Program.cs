using System;
using System.Numerics;

namespace p13_Factorial
{
    class Program
    {
        static void Main(string[] args)
        {
            int num = int.Parse(Console.ReadLine());
            BigInteger factorial = GetFactorial(num);
            Console.WriteLine(factorial);
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
