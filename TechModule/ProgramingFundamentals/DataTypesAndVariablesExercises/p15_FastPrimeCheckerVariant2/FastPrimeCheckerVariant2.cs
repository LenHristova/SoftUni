using System;

namespace p15_FastPrimeCheckerVariant2
{
    class FastPrimeCheckerVariant2
    {
        static void Main(string[] args)
        {
            int lastNumOtfTheRange = int.Parse(Console.ReadLine());

            for (int currentNum = 2; currentNum <= lastNumOtfTheRange; currentNum++)
            {
                bool isPrime = IsPrime(currentNum);

                Console.WriteLine($"{currentNum} -> {isPrime}");
            }
        }

        private static bool IsPrime(int currentNum)
        {
            for (int divider = 2; divider <= Math.Sqrt(currentNum); divider++)
            {
                if (currentNum % divider == 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
