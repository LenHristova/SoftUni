using System;

namespace p03_EnglishNameOfLastDigit
{
    class Program
    {
        static void Main(string[] args)
        {
            long number = long.Parse(Console.ReadLine());
            string englishNameOfLastDigit = GetEnglishNameOfLastDigit(number);
            Console.WriteLine(englishNameOfLastDigit);
        }

        private static string GetEnglishNameOfLastDigit(long number)
        {
            int lastDigit = (int)Math.Abs(number % 10);

            if (lastDigit == 1)
            {
                return "one";
            }
            else if (lastDigit == 2)
            {
                return "two";
            }
            else if (lastDigit == 3)
            {
                return "three";
            }
            else if (lastDigit == 4)
            {
                return "four";
            }
            else if (lastDigit == 5)
            {
                return "five";
            }
            else if (lastDigit == 6)
            {
                return "six";
            }
            else if (lastDigit == 7)
            {
                return "seven";
            }
            else if (lastDigit == 8)
            {
                return "eight";
            }
            else if (lastDigit == 9)
            {
                return "nine";
            }

            return "zero";
        }
    }
}
