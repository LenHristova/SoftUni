using System;

using P04_WorkForce.Contracts;

namespace P04_WorkForce.IO
{
    public class ConsoleWriter : IWriter
    {
        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }

        public void WriteLine(int message)
        {
            Console.WriteLine(message);
        }

        public void Write(string message)
        {
            Console.Write(message);
        }

        public void Write(int message)
        {
            Console.Write(message);
        }
    }
}