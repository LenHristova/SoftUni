using System;
using System.Linq;

namespace MagicExchangeableWords
{
    class StartUp
    {
        static void Main()
        {
            string[] twoStrings = Console.ReadLine()
                .Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);

            var str1 = twoStrings[0].Distinct().ToArray();
            var str2 = twoStrings[1].Distinct().ToArray();

            if (str1.Length == str2.Length)
            {
                Console.WriteLine("true");
            }
            else
            {
                Console.WriteLine("false");
            }
        }
    }
}
