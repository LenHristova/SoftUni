using System;
using System.Collections.Generic;
using System.Linq;

class StartUp
{
    static void Main()
    {
        var numbers = Console.ReadLine()
            .Split(new[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToArray();

        Array.Sort(numbers, new CustomComperator());

        Console.WriteLine(string.Join(" ", numbers));
    }

    class CustomComperator : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            if (x % 2 == 0 && y % 2 != 0)
                return -1;

            if (x % 2 != 0 && y % 2 == 0)
                return 1;

            if (x < y)
                return -1;

            if (x > y)
                return 1;

            return 0;
        }
    }
}