using System;
using System.Text;

namespace UnicodeCharacters
{
    class StartUp
    {
        static void Main()
        {
            string str = Console.ReadLine();

            StringBuilder sb = new StringBuilder();
            foreach (var ch in str)
            {
                sb.Append($"\\u{(int)ch:x4}");
            }

            Console.WriteLine(sb);
        }
    }
}