using System;

namespace PascalTriangle
{
    class StartUp
    {
        static void Main()
        {
            var height = int.Parse(Console.ReadLine());

            var jagged = new long[height][];
            for (int row = 0; row < height; row++)
            {
                jagged[row] = new long[row + 1];
                jagged[row][0] = 1;
                jagged[row][row] = 1;

                for (int col = 1; col < row; col++)
                {
                    jagged[row][col] = jagged[row - 1][col - 1] + jagged[row - 1][col];
                }
            }

            foreach (var longArr in jagged)
            {
                Console.WriteLine(string.Join(" ", longArr));
            }
        }
    }
}
