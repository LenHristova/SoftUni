using System;

using DungeonsAndCodeWizards.Contracts;

namespace DungeonsAndCodeWizards.IO
{
    public class ConsoleReader : IReader
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}