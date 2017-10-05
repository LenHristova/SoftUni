using System;
using System.Text;

namespace p04_NumbersInReversedOrder
{
    class Program
    {
        static void Main(string[] args)
        {
            string number = Console.ReadLine();
            Console.WriteLine(ReverseNumber(number));
        }

        private static StringBuilder ReverseNumber(string number)
        {
            StringBuilder reversedNumber = new StringBuilder();
            for (int i = number.Length - 1; i >= 0; i--)
            {
                reversedNumber.Append(number[i]);
            }
            return reversedNumber;
        }
    }
}
