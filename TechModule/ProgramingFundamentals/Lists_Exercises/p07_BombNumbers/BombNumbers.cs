using System;
using System.Collections.Generic;
using System.Linq;

namespace p07_BombNumbers
{
    class BombNumbers
    {
        static void Main()
        {
            List<int> numbers = Console.ReadLine()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();
            int[] specialBomb = Console.ReadLine()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            int bomb = specialBomb[0];
            int power = specialBomb[1];

            while (numbers.Contains(bomb))
            {
                int bombIndex = numbers.IndexOf(bomb);
                int leftIndex = Math.Max(bombIndex - power, 0);
                int rightIndex = Math.Min(bombIndex + power, numbers.Count - 1);
                numbers.RemoveRange(leftIndex, rightIndex - leftIndex + 1);
            }

            Console.WriteLine(numbers.Sum());
        }
    }
}
