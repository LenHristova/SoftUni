using System;
using System.IO;
using System.Linq;

namespace MostFrequentNumber
{
    class StartUp
    {
        static void Main()
        {
            File.Delete(@"..\..\output.txt");

            var lines = File.ReadAllLines(@"..\..\input.txt");
            foreach (var line in lines)
            {
                var res = line
                    .Split(new[] { ' ' },
                        StringSplitOptions.RemoveEmptyEntries)
                    .GroupBy(n => n)
                    .OrderByDescending(gr => gr.Count())
                    .First()
                    .Key;

                File.AppendAllText(@"..\..\output.txt", $"{res}{Environment.NewLine}");
            }
        }
    }
}
