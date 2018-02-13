using System;
using System.Linq;

namespace SquaresInMatrix
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

            var matrix = GetMatrix(rows, cols);

            var squaresCount = 0;


            for (int row = 0; row < rows - 1; row++)
            {
                for (int col = 0; col <cols - 1; col++)
                {
                    if (HasSquare(matrix, row, col))
                    {
                        squaresCount++;
                    }
                }
            }

            Console.WriteLine(squaresCount);
        }

        private static bool HasSquare(char[,] matrix, int row, int col)
        {
            return matrix[row, col] == matrix[row, col + 1] &&
                   matrix[row, col] == matrix[row + 1, col] &&
                   matrix[row, col] == matrix[row + 1, col + 1];
        }

        private static char[,] GetMatrix(int rows, int cols)
        {
            var matrix = new char[rows, cols];

            for (int row = 0; row < rows; row++)
            {
                var currentRowNums = Console.ReadLine()
                    .Split(new[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                for (int col = 0; col < cols; col++)
                {
                    matrix[row, col] = currentRowNums[col][0];
                }
            }

            return matrix;
        }
    }
}
