namespace BusTickets.Client.Core.IO
{
    using System;
    using Contracts;

    internal class ConsoleWriter : IWriter
    {
        public void Write(string output) => Console.Write(output);

        public void WriteLine(string output)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(output);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void WriteLine() => Console.WriteLine();

        public void WriteErrorMessage(string output)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(output);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void Clear() => Console.Clear();
    }
}