using System;

namespace p01_HelloName
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Hello, {GetName()}!");
        }

        private static string GetName()
        {
            return Console.ReadLine();
        }
    }
}
