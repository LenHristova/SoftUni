using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterMultiplier
{
    class StartUp
    {
        static void Main()
        {
            string[] strings = Console.ReadLine()
                .Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            string str1 = strings[0];
            string str2 = strings[1];

            int sum = SumCharactersCodesMultiplied(str1, str2);

            Console.WriteLine(sum);
        }

        private static int SumCharactersCodesMultiplied(string str1, string str2)
        {
            string longerStr = str1.Length >= str2.Length ? str1 : str2;
            string smallerStr = str1.Length < str2.Length ? str1 : str2;

            int sum = 0;
            for (int i = 0; i < longerStr.Length; i++)
            {
                if (i < smallerStr.Length)
                {
                    sum += longerStr[i] * smallerStr[i];
                }
                else
                {
                    sum += longerStr[i];
                }
            }

            return sum;
        }
    }
}
