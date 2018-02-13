using System;
using System.Linq;

namespace LegoBlocks
{
    class StartUp
    {
        private static int _cellsCount;

        static void Main()
        {
            var rows = int.Parse(Console.ReadLine());

            var jagged1 = GetJagged(rows);
            var jagged2 = GetJagged(rows);

            if (AreFit(jagged1, jagged2))
            {
                ReverseEveryRow(jagged2);
                PrintJaggeds(jagged1, jagged2);
            }
            else
            {
                Console.WriteLine($"The total number of cells is: {_cellsCount}");
            }
        }

        private static void PrintJaggeds(string[][] jagged1, string[][] jagged2)
        {
            for (int row = 0; row < jagged1.Length; row++)
            {
                Console.WriteLine($"[{string.Join(", ", jagged1[row])}, {string.Join(", ", jagged2[row])}]");
            }
        }

        private static void ReverseEveryRow(string[][] jagged2)
        {
            for (int row = 0; row < jagged2.Length; row++)
            {
                jagged2[row] = jagged2[row]
                    .Reverse()
                    .ToArray();
            }
        }

        private static bool AreFit(string[][] jagged1, string[][] jagged2)
        {
            var fittingLenght = jagged1[0].Length + jagged2[0].Length;
            for (int row = 1; row < jagged1.Length; row++)
            {
                var currentRowLength = jagged1[row].Length + jagged2[row].Length;
                if (fittingLenght != currentRowLength)
                {
                    return false;
                }
            }

            return true;
        }

        private static string[][] GetJagged(int rows)
        {
            var jagged = new string[rows][];
            for (int row = 0; row < rows; row++)
            {
                var currentArr = Console.ReadLine()
                    .Split(new[] {' ', '\t', '\n'}, StringSplitOptions.RemoveEmptyEntries);
                jagged[row] = currentArr;

                _cellsCount += currentArr.Length;
            }

            return jagged;
        }
    }
}
