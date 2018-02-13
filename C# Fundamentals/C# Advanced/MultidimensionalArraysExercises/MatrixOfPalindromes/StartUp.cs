using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace MatrixOfPalindromes
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var rowsCols = Console.ReadLine()
                .Split(new[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var rows = rowsCols[0];
            var cols = rowsCols[1];

            var matrix = GetMatrix(rows, cols);

            Print(matrix);
        }

        private static void Print(string[,] matrix)
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    Console.Write(matrix[row, col] + " ");
                }

                Console.WriteLine();
            }
        }


        private static string[,] GetMatrix(int rows, int cols)
        {
            var matrix = new string[rows, cols];

            var alphabet = "abcdefghijklmnopqrstuvwxyz".ToCharArray();

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    var currentPalindrome = "" + alphabet[row] + alphabet[row + col] + alphabet[row];

                    matrix[row, col] = currentPalindrome.ToString();
                }
            }

            return matrix;
        }
    }
}
