using System;

namespace p17_PrintPartOfAsciiTable
{
    class PrintPartOfAsciiTable
    {
        static void Main(string[] args)
        {
            int firstSymbol = int.Parse(Console.ReadLine());
            int lastSymbol = int.Parse(Console.ReadLine());

            for (int currentSymbol = firstSymbol; currentSymbol <= lastSymbol; currentSymbol++)
            {
                Console.Write((char)currentSymbol + " ");
            }
            Console.WriteLine();
        }
    }
}
