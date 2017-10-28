using System;
using System.Linq;
using System.Text;

namespace SumBigNumbers
{
    class StartUp
    {
        static void Main()
        {
            string num1 = Console.ReadLine();
            string num2 = Console.ReadLine();

            string result = Sum(num1, num2);

            Console.WriteLine(result);
        }

        private static string Sum(string num1, string num2)
        {
            if (num1.Contains('.') || num2.Contains('.'))
            {
                string[] num1Parts = GetIntegerAndFractionalParts(num1);
                string[] num2Parts = GetIntegerAndFractionalParts(num2);

                int longestIntegerPart = Math.Max(num1Parts[0].Length, num2Parts[0].Length) + 1;
                int longestFractionalPart = Math.Max(num1Parts[1].Length, num2Parts[1].Length);

                num1 = GetNum(num1Parts, longestIntegerPart, longestFractionalPart);
                num2 = GetNum(num2Parts, longestIntegerPart, longestFractionalPart);
            }
            else
            {
                int resultMaxDigitsCount = Math.Max(num1.Length, num2.Length) + 1;
                num1 = num1.PadLeft(resultMaxDigitsCount, '0');
                num2 = num2.PadLeft(resultMaxDigitsCount, '0');
            }

            int reminder = 0;
            StringBuilder sb = new StringBuilder();
            for (int i = num1.Length - 1; i >= 0; i--)
            {
                if (num1[i] == '.')
                {
                    sb.Insert(0, '.');
                    continue;
                }

                string sumDigits = $"{(num1[i] - 48 + num2[i] - 48 + reminder):D2}";

                reminder = int.Parse(sumDigits[0].ToString());
                sb.Insert(0, sumDigits[1]);
            }

            string result = sb.ToString().TrimStart('0');
            return result.Contains('.') ? result.TrimEnd('.', '0') : result;
        }

        //Fill the differences in length of parts with zeros
        private static string GetNum(string[] numParts, int longestIntegerPart, int longestFractionalPart)
        {
            return numParts[0].PadLeft(longestIntegerPart, '0') + "." +
                numParts[1].PadRight(longestFractionalPart, '0');
        }

        private static string[] GetIntegerAndFractionalParts(string num)
        {

            if (num.Contains('.'))
            {
                return num
                    .Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            }

            return new string[2]
                {num, "0"};
        }
    }
}