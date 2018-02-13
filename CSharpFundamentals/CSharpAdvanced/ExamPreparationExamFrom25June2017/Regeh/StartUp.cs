using System;
using System.Text.RegularExpressions;

namespace Regeh
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine();

            var pattern = @"\[[^\[]+<([0-9]+)REGEH([0-9]+)>[^\]]+]";

            var matches = Regex.Matches(input, pattern);
            var output = string.Empty;
            var sumIndeces = 0;
            foreach (Match match in matches)
            {
                var index1 = int.Parse(match.Groups[1].Value);
                GetSymbol(index1, ref sumIndeces, input, ref output);

                var index2 = int.Parse(match.Groups[2].Value);
                GetSymbol(index2, ref sumIndeces, input, ref output);
            }

            Console.WriteLine(output);
        }

        private static void GetSymbol(int index, ref int sumIndeces, string input, ref string output)
        {
            sumIndeces += index;
            var symbol = input[sumIndeces % input.Length];
            output += symbol;
        }
    }
}