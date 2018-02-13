using System;
using System.Linq;

namespace _01_SumMatrixElements
{
    class StartUp
    {
        static void Main()
        {
            var matrixSize = Console.ReadLine()
                .Split(new[] {", "}, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var rows = matrixSize[0];
            var cols = matrixSize[1];

            var matrix = new int[rows, cols];

            var sum = 0;
            for (var row = 0; row < rows; row++)
            {
                var currentRow = Console.ReadLine()
                    .Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                for (var col = 0; col < cols; col++)
                {
                    matrix[row, col] = currentRow[col];
                    sum += currentRow[col];
                }
            }

            Console.WriteLine(rows);
            Console.WriteLine(cols);
            Console.WriteLine(sum);
        }
    }
}
