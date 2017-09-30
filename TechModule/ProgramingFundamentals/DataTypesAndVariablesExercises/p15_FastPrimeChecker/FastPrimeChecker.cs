using System;

namespace p15_FastPrimeChecker
{
    class FastPrimeChecker
    {
        static void Main(string[] args)
        {
            int lastNumOtfTheRange = int.Parse(Console.ReadLine());

            for (int currentNum = 2; currentNum <= lastNumOtfTheRange; currentNum++)
            {
                bool isPrime = true;

                for (int divider = 2; divider <= Math.Sqrt(currentNum); divider++)
                {
                    if (currentNum % divider == 0)
                    {
                        isPrime = false;
                    }
                }
                Console.WriteLine($"{currentNum} -> {isPrime}");
            }
        }
    }
}
