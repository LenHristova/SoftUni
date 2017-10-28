using System;
using System.Text;

namespace ReverseString2
{
    class StartUp
    {
        static void Main()
        {
            var str = Console.ReadLine();
            var reversed = ReverseString(str);
            Console.WriteLine(reversed);
        }

        private static string ReverseString(string str)
        {
            var sb = new StringBuilder();
            for (int i = str.Length - 1; i >= 0; i--)
            {
                sb.Append(str[i]);
            }

            return sb.ToString();
        }
    }
}