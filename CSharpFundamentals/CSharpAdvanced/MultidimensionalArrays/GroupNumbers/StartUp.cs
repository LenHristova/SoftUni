using System;
using System.Linq;

namespace GroupNumbers
{
    class StartUp
    {
        static void Main()
        {
            var numbers = Console.ReadLine()
                .Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var groupsCounts = new int[3];

            foreach (var num in numbers)
            {
                groupsCounts[Math.Abs(num % 3)]++;
            }

            var jagged = new int[3][]
            {
                new int[groupsCounts[0]],
                new int[groupsCounts[1]],
                new int[groupsCounts[2]],
            };

            var pos = new int[3];
            foreach (var num in numbers)
            {
                var rem = Math.Abs(num % 3);
                jagged[rem][pos[rem]++] = num;
            }

            foreach (var intArr in jagged)
            {
                Console.WriteLine(string.Join(" ", intArr));
            }
        }
    }
}
