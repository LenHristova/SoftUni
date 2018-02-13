using System;
using System.Linq;

namespace MaximalSum
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var size = Console.ReadLine()
                .Split(new[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var rows = size[0];
            var cols = size[1];

            if (rows < 3 || cols < 3)
            {
                return;
            }

            var matrix = GetMatrix(rows, cols);

            var maxSum = 0;
            var maxSumRow = 0;
            var maxSumCol = 0;

            for (int row = 0; row < rows - 2; row++)
            {
                for (int col = 0; col < cols - 2; col++)
                {
                    var currentSquareSum = CalcSum(matrix, row, col);

                    if (currentSquareSum > maxSum)
                    {
                        maxSum = currentSquareSum;
                        maxSumRow = row;
                        maxSumCol = col;
                    }
                }

            }

            Console.WriteLine("Sum = " + maxSum);
            PrintMaxSquare(matrix, maxSumRow, maxSumCol);
        }

        private static void PrintMaxSquare(int[,] matrix, int maxSumRow, int maxSumCol)
        {
            for (int row = maxSumRow; row < maxSumRow + 3; row++)
            {
                for (int col = maxSumCol; col < maxSumCol + 3; col++)
                {
                    Console.Write(matrix[row, col] + " ");
                }

                Console.WriteLine();
            }
        }

        private static int CalcSum(int[,] matrix, int row, int col)
        {
            var sum = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    sum += matrix[row + i, col + j];
                }
            }

            return sum;
        }

        private static int[,] GetMatrix(int rows, int cols)
        {
            var matrix = new int[rows, cols];

            for (int row = 0; row < rows; row++)
            {
                var currentRowNums = Console.ReadLine()
                    .Split(new[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                for (int col = 0; col < cols; col++)
                {
                    matrix[row, col] = currentRowNums[col];
                }
            }

            return matrix;
        }
    }
}
