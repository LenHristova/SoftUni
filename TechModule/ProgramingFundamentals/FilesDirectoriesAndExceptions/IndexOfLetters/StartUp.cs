using System;
using System.IO;

namespace IndexOfLetters
{
    class StartUp
    {
        static void Main()
        {
            File.Delete(@"..\..\output.txt");

            var lines = File.ReadAllLines(@"..\..\input.txt");

            foreach (var line in lines)
            {
                foreach (var ch in line.ToCharArray())
                {
                    File.AppendAllText(@"..\..\output.txt",
                        $"{ch} -> {ch - 97}{Environment.NewLine}");
                }

                File.AppendAllText(@"..\..\output.txt", "------------" + Environment.NewLine);
            }
        }
    }
}
