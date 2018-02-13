using System;
using System.Linq;

namespace RubikMatrix
{
    class StartUp
    {
        private static int[][] positions;

        static void Main(string[] args)
        {
            var size = Console.ReadLine()
                .Split(new[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var rows = size[0];
            var cols = size[1];

            var matrix = GetMatrix(rows, cols);
            positions = new int[matrix.Length + 1][];

            ExecuteCommands(matrix);

            Rearrange(matrix);
        }

        private static void Rearrange(int[,] matrix)
        {
            var numPosition = 1;
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {

                    if (matrix[row, col] == numPosition)
                    {
                        Console.WriteLine("No swap required");
                    }
                    else
                    {
                        var neededNumRowPos = positions[numPosition][0];
                        var neededNumColPos = positions[numPosition][1];

                        var currCellNum = matrix[row, col];
                        matrix[neededNumRowPos, neededNumColPos] = currCellNum;
                        positions[currCellNum] = new[] {neededNumRowPos, neededNumColPos};
                        matrix[row, col] = numPosition;

                        Console.WriteLine($"Swap ({row}, {col}) with ({neededNumRowPos}, {neededNumColPos})");
                    }

                    numPosition++;

                }
            }
        }

        private static void ExecuteCommands(int[,] matrix)
        {
            var commandsCount = int.Parse(Console.ReadLine());

            for (int i = 0; i < commandsCount; i++)
            {
                var commandsArgs = Console.ReadLine()
                    .Split(new[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                var command = commandsArgs[1];

                switch (command)
                {
                    case "up":
                        SwapToUp(matrix, commandsArgs);
                        break;
                    case "down":
                        SwapToDown(matrix, commandsArgs);
                        break;
                    case "left":
                        SwapToLeft(matrix, commandsArgs);
                        break;
                    case "right":
                        SwapToRight(matrix, commandsArgs);
                        break;
                }
            }
        }

        private static void SwapToRight(int[,] matrix, string[] commandsArgs)
        {
            var row = int.Parse(commandsArgs[0]);
            var moves = int.Parse(commandsArgs[2]) % matrix.GetLength(1);

            for (int i = 0; i < moves; i++)
            {
                var temp = matrix[row, matrix.GetLength(1) - 1];
                for (int col = matrix.GetLength(1) - 1; col > 0; col--)
                {
                    var newCol = col - 1;
                    var swappedNum = matrix[row, newCol];
                    positions[swappedNum] = new[] { row, col };
                    matrix[row, col] = swappedNum;
                }
                positions[temp] = new[] { row, 0 };
                matrix[row, 0] = temp;
            }
        }

        private static void SwapToLeft(int[,] matrix, string[] commandsArgs)
        {
            var row = int.Parse(commandsArgs[0]);
            var moves = int.Parse(commandsArgs[2]) % matrix.GetLength(1);

            for (int i = 0; i < moves; i++)
            {
                var temp = matrix[row, 0];
                for (int col = 0; col < matrix.GetLength(1) - 1; col++)
                {
                    var newCol = col + 1;
                    var swappedNum = matrix[row, newCol];
                    positions[swappedNum] = new[] { row, col };
                    matrix[row, col] = swappedNum;
                }
                positions[temp] = new[] { row, matrix.GetLength(1) - 1 };
                matrix[row, matrix.GetLength(1) - 1] = temp;
            }
        }

        private static void SwapToDown(int[,] matrix, string[] commandsArgs)
        {
            var col = int.Parse(commandsArgs[0]);
            var moves = int.Parse(commandsArgs[2]) % matrix.GetLength(0);

            for (int i = 0; i < moves; i++)
            {
                var temp = matrix[matrix.GetLength(0) - 1, col];
                for (int row = matrix.GetLength(0) - 1; row > 0; row--)
                {
                    var newRow = row - 1;
                    var swappedNum = matrix[newRow, col];
                    positions[swappedNum] = new[] { row, col };
                    matrix[row, col] = swappedNum;
                }
                positions[temp] = new[] { 0, col };
                matrix[0, col] = temp;
            }
        }

        private static void SwapToUp(int[,] matrix, string[] commandsArgs)
        {
            var col = int.Parse(commandsArgs[0]);
            var moves = int.Parse(commandsArgs[2]) % matrix.GetLength(0);

            for (int i = 0; i < moves; i++)
            {
                var temp = matrix[0, col];
                for (int row = 0; row < matrix.GetLength(0) - 1; row++)
                {
                    var newRow = row + 1;
                    var swappedNum = matrix[newRow, col];
                    positions[swappedNum] = new[] { row, col };
                    matrix[row, col] = swappedNum;
                }

                positions[temp] = new[] { matrix.GetLength(0) - 1, col };
                matrix[matrix.GetLength(0) - 1, col] = temp;
            }
        }

        private static int[,] GetMatrix(int rows, int cols)
        {
            var matrix = new int[rows, cols];

            var number = 1;
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    matrix[row, col] = number++;
                }
            }

            return matrix;
        }
    }
}
