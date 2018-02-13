using System;
using System.Linq;

namespace FindEvensOrOdds
{
    class StartUp
    {
        static void Main()
        {
            var bounds = Console.ReadLine()
                .Split(new[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();

            var lowerBound = bounds[0];
            var upperBound = bounds[1];
            var rangeCount = upperBound - lowerBound + 1;
            var command = Console.ReadLine();

            var list = Enumerable.Range(lowerBound, rangeCount)
            .Where(Command(command))
            .ToList();

            Console.WriteLine(string.Join(" ", list));
        }

        static Func<int, bool> Command(string command)
        {
            switch (command)
            {
                case "odd":
                    return n => n % 2 != 0;
                case "even":
                    return n => n % 2 == 0;
                default:
                    throw new NotSupportedException(@"Command must be ""odd"" or ""even""!");
            }
        }
    }
}
