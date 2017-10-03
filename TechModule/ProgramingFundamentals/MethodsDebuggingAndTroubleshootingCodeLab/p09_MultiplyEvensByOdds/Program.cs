using System;

namespace p09_MultiplyEvensByOdds
{
    class Program
    {
        static void Main(string[] args)
        {
            int num = int.Parse(Console.ReadLine());
            int evenDigitsSum = GetSum(num, 0);
            int oddDigitsSum = GetSum(num, 1);
            Console.WriteLine(evenDigitsSum * oddDigitsSum);
        }

        private static int GetSum(int num, int remainder)
        {
            int sum = 0;
            while (num != 0)
            {
                int currentDigit = num % 10;
                if (Math.Abs(currentDigit % 2) == remainder)
                {
                    sum += currentDigit;
                }
                num /= 10;
            }
            return sum;
        }
    }
}
