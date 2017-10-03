using System;

namespace p02_SignOfIntegerNumber
{
    class Program
    {
        static void Main(string[] args)
        {
            int num = int.Parse(Console.ReadLine());

            PrintNumSign(num);
        }

        private static void PrintNumSign(int num)
        {
            if (num == Math.Abs(num))
            {
                if (num == 0)
                {
                    Console.WriteLine("The number 0 is zero.");
                }
                else
                {
                    Console.WriteLine($"The number {num} is positive.");
                }
            }
            else
            {
                Console.WriteLine($"The number {num} is negative.");
            }
        }
    }
}
