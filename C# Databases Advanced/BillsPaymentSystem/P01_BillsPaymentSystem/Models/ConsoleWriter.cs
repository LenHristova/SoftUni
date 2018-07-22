namespace P01_BillsPaymentSystem.Models
{
    using System;
    using Contracts.Models;

    public class ConsoleWriter : IWriter
    {
        public void Write(string output) => Console.Write(output);

        public void WriteLine(string output) => Console.WriteLine(output);

        public void WriteLine() => Console.WriteLine();

        public void WriteSpecialMessage(string output)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(output);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void WriteHelperMessage(string output)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(output);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void WriteErrorMessage(string output)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(output);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void Clear() => Console.Clear();
    }
}