using System;

namespace p03_EnglishNameOfLastDigitVariant2
{
    class Program
    {
        static void Main(string[] args)
        {
            long numebr = long.Parse(Console.ReadLine());
            Console.WriteLine(GetEnglishNameOfLastDigit(numebr));
        }

        private static string GetEnglishNameOfLastDigit(long numebr)
        {
            string[] englishLastDigit = { "zero", "one", "two", "three",
            "four", "five", "six", "seven", "eight", "nine" };
            return englishLastDigit[Math.Abs(numebr % 10)];
        }
    }
}
