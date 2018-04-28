using System;

using DungeonsAndCodeWizards.Contracts;

namespace DungeonsAndCodeWizards.IO
{
    public class ConsoleWriter : IWriter
    {
        public void WriteLine(string output)
        {
           // File.AppendAllText("result.txt", output + Environment.NewLine);
            Console.WriteLine(output);
        }
    }
}