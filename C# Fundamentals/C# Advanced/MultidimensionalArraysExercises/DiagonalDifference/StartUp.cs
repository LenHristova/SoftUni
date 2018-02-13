using System;
using System.Linq;

namespace DiagonalDifference
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var size = int.Parse(Console.ReadLine());
            var matrix = GetMatrix(size);

            var leftDiagonalSum = 0;
            var rightDiagonalSum = 0;

            for (int i = 0; i < size; i++)
            {
                leftDiagonalSum += matrix[i, i];
                rightDiagonalSum += matrix[i, size - 1 - i];
            }

            var diff = Math.Abs(leftDiagonalSum - rightDiagonalSum);
            Console.WriteLine(diff);
        }

        private static int[,] GetMatrix(int size)
        {
            var matrix = new int[size, size];

            for (int row = 0; row < size; row++)
            {
                var currentRowNums = Console.ReadLine()
                    .Split(new[] {' ', '\t', '\n'}, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                for (int col = 0; col < size; col++)
                {
                    matrix[row, col] = currentRowNums[col];
                }
            }

            return matrix;
        }
    }
}
