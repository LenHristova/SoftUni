using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace p07_TakeSkipRope
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            int[] digits = input
                .ToCharArray()
                .Where(char.IsDigit)
                .Select(ch => int.Parse(ch.ToString()))
                .ToArray();
            char[] chars = input
                .ToCharArray()
                .Where(ch => !char.IsDigit(ch))
                .ToArray();

            int[] takeList = digits
                .Where((n, index) => index % 2 == 0)
                .ToArray();
            int[] skipList = digits
                .Where((n, index) => index % 2 != 0)
                .ToArray();

            string decryptedMessage = string.Empty;
            int currSkipCount = 0;
            for (int i = 0; i < takeList.Length; i++)
            {
                decryptedMessage += string.Join("", chars
                    .Skip(currSkipCount)
                    .Take(takeList[i]));
                currSkipCount += skipList[i] + takeList[i];
            }
            Console.WriteLine(decryptedMessage);
        }
    }
}
