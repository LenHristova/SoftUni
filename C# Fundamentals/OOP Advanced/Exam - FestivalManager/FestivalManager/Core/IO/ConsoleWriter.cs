using System;
using System.IO;

using FestivalManager.Core.IO.Contracts;

namespace FestivalManager.Core.IO
{
    public class ConsoleWriter : IWriter
    {
        public void WriteLine(string contents)
        {
            //File.AppendAllText("result.txt", contents + Environment.NewLine);
            Console.WriteLine(contents);
        }

        public void Write(string contents)
        {
            Console.Write(contents);
        }
    }
}