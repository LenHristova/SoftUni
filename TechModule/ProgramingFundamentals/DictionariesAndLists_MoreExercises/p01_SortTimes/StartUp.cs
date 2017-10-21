using System;
using System.Collections.Generic;
using System.Linq;

namespace p01_SortTimes
{
    class StartUp
    {
        static void Main(string[] args)
        {
            string[] times = Console.ReadLine()
                .Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries)
                .OrderBy(s => s)
                .ToArray();
            Console.WriteLine(string.Join(", ", times));
        }
    }
}
