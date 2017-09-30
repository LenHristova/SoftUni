using System;

namespace p05_BooleanVariable
{
    class BooleanVariable
    {
        static void Main(string[] args)
        {
            string answer = Console.ReadLine() == "True" ? "Yes" : "No";
            Console.WriteLine(answer);
        }
    }
}
