using System;
using System.ComponentModel;
using System.Linq;

namespace SquareWithMaximumSum
{
    class StartUp
    {
        static void Main()
        {
            var matrix = GetMatrix();

            var maxSum = 0L;
            var maxSumFirstCellRow = 0;
            var maxSumFirstCellCol = 0;

            for (int row = 0; row < matrix.GetLength(0) - 1; row++)
            {
                for (int col = 0; col < matrix.GetLength(1) - 1; col++)
                {
                    var currentSum = matrix[row, col] + matrix[row, col + 1] +
                                     matrix[row + 1, col] + matrix[row + 1, col + 1];

                    if (currentSum > maxSum)
                    {
                        maxSum = currentSum;
                        maxSumFirstCellRow = row;
                        maxSumFirstCellCol = col;
                    }
                }
            }

            var firstLeftCell = matrix[maxSumFirstCellRow, maxSumFirstCellCol];
            var firstRightCell = matrix[maxSumFirstCellRow, maxSumFirstCellCol + 1];
            var secondLeftCell = matrix[maxSumFirstCellRow + 1, maxSumFirstCellCol];
            var secondRightCell = matrix[maxSumFirstCellRow + 1, maxSumFirstCellCol + 1];
            Console.WriteLine(firstLeftCell + " " + firstRightCell);
            Console.WriteLine(secondLeftCell + " " + secondRightCell);
            Console.WriteLine(maxSum);
        }

        private static int[,] GetMatrix()
        {
            var matrixSize = Console.ReadLine()
                .Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var rows = matrixSize[0];
            var cols = matrixSize[1];
            var matrix = new int[rows, cols];

            for (var row = 0; row < rows; row++)
            {
                var currentRow = Console.ReadLine()
                    .Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                for (var col = 0; col < cols; col++)
                {
                    matrix[row, col] = currentRow[col];
                }
            }

            return matrix;
        }
    }
}