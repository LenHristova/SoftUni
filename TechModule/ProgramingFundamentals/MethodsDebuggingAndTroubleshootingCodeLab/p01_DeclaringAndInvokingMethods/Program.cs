using System;

namespace p01_DeclaringAndInvokingMethods
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintHeader();
            PrintBody();
            PrintFooter();
        }

        private static void PrintHeader()
        {
            Console.WriteLine("CASH RECEIPT");
            Console.WriteLine(new String('-', 30));
        }

        private static void PrintBody()
        {
            Console.WriteLine("Charged to" + new String('_', 20));
            Console.WriteLine("Received by" + new String('_', 19));
        }

        private static void PrintFooter()
        {
            Console.WriteLine(new String('-', 30));
            Console.WriteLine("\u00A9 SoftUni");
        }
    }
}
