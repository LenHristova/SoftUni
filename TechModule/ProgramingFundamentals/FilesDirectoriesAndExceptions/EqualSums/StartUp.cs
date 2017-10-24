using System;
using System.IO;
using System.Linq;

namespace EqualSums
{
    class StartUp
    {
        static void Main()
        {
            var lines = File.ReadAllLines(@"..\..\input.txt");

            File.Delete(@"..\..\output.txt");
            foreach (var line in lines)
            {
                var numbers = line
                    .Split(new[] { ' ' },
                        StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToList();

                for (var pos = 0; pos < numbers.Count; pos++)
                {
                    if (numbers.Take(pos).Sum() == numbers.Skip(pos + 1).Sum())
                    {
                        File.AppendAllText(@"..\..\output.txt", $"{pos}" + Environment.NewLine);
                        break;
                    }

                    if (pos == numbers.Count - 1)
                    {
                        File.AppendAllText(@"..\..\output.txt", "no" + Environment.NewLine);
                    }
                }
            }
        }
    }
}
