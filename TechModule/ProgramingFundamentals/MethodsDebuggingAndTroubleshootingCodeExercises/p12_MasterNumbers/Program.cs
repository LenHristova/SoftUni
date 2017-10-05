using System;
using System.Collections.Generic;

namespace p12_MasterNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            int rangeLastNum = int.Parse(Console.ReadLine());
            List<int> masterNumbers = new List<int>();
            AddMasterNumbers(masterNumbers, rangeLastNum);
            Console.WriteLine(string.Join(Environment.NewLine, masterNumbers));
        }

        private static void AddMasterNumbers(List<int> masterNumbers, int rangeLastNum)
        {
            for (int currentNum = 232; currentNum <= rangeLastNum; currentNum++)
            {
                if (IsPalindrom(currentNum.ToString()))
                {
                    if (IsSumOfDigitsDivisibleBy7(currentNum))
                    {
                        if (HasAtLeastOneEvenDigit(currentNum))
                        {
                            masterNumbers.Add(currentNum);
                        }
                    }
                }
            }
        }

        private static bool HasAtLeastOneEvenDigit(int num)
        {
            while (num > 0)
            {
                if ((num % 10) % 2 == 0)
                {
                    return true;
                }
                num /= 10;
            }
            return false;
        }

        private static bool IsSumOfDigitsDivisibleBy7(int num)
        {
            int sum = 0;
            while (num > 0)
            {
                sum += num % 10;
                num /= 10;
            }

            if (sum % 7 == 0)
            {
                return true;
            }
            return false;
        }

        private static bool IsPalindrom(string numToString)
        {
            for (int i = 0; i < numToString.Length / 2; i++)
            {
                if (numToString[i] != numToString[numToString.Length - 1 - i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
