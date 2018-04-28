using System;

using P04_WorkForce.Contracts;

namespace P04_WorkForce.IO
{
    public class ConsoleReader : IReader
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}