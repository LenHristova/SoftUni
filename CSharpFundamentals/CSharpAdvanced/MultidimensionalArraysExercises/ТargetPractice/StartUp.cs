using System;
using System.Collections.Generic;
using System.Linq;

namespace ТargetPractice
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var dimensions = Console.ReadLine()
                .Split(new[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var rows = dimensions[0];
            var cols = dimensions[1];

            var snakeStr = Console.ReadLine();

            var shotParams = Console.ReadLine()
                .Split(new[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var impactRow = shotParams[0];
            var impactCol = shotParams[1];
            var radius = shotParams[2];

            var stairesMatrix = new char[rows, cols];

            PutSnakesToTheStairs(rows, cols, snakeStr, stairesMatrix);

            Destroy(impactRow, impactCol, radius, stairesMatrix);

            Drop(stairesMatrix);

            PrintStairsMatrixAfterDestruction(stairesMatrix);
        }       

        private static void PutSnakesToTheStairs(int rows, int cols, string snakeStr, char[,] stairesMatrix)
        {
            var isLeft = true;
            var currSymbolCounter = 0;
            for (int row = rows - 1; row >= 0; row--)
            {
                for (int col = 0; col < cols; col++)
                {
                    if (isLeft)
                    {
                        stairesMatrix[row, cols - col - 1] = snakeStr[currSymbolCounter % snakeStr.Length];
                    }
                    else
                    {
                        stairesMatrix[row, col] = snakeStr[currSymbolCounter % snakeStr.Length];
                    }

                    currSymbolCounter++;
                }
                isLeft = isLeft != true;
            }
        }

        private static void Destroy(int impactRow, int impactCol, int radius, char[,] stairesMatrix)
        {
            for (int row = 0; row < stairesMatrix.GetLength(0); row++)
            {
                for (int col = 0; col < stairesMatrix.GetLength(1); col++)
                {
                    if (IsInDestroyScope(row, col, impactRow, impactCol, radius, stairesMatrix))
                    {
                        stairesMatrix[row, col] = ' ';
                    }
                }
            }
        }

        private static bool IsInDestroyScope(int row, int col, int impactRow, int impactCol, int radius, char[,] stairesMatrix)
        {
            return Math.Pow((row - impactRow), 2) + Math.Pow((col - impactCol), 2) <= Math.Pow(radius, 2);
        }

        private static void Drop(char[,] stairesMatrix)
        {
            for (int col = 0; col < stairesMatrix.GetLength(1); col++)
            {
                var row = stairesMatrix.GetLength(0) - 1;
                while (row >= 0)
                {
                    if (stairesMatrix[row, col] == ' ')
                    {
                        var notDestroyedCellRow = row - 1;
                        while (notDestroyedCellRow != -1 &&
                               stairesMatrix[notDestroyedCellRow, col] == ' ')
                        {
                            notDestroyedCellRow--;
                        }

                        while (notDestroyedCellRow >= 0)
                        {
                            stairesMatrix[row, col] = stairesMatrix[notDestroyedCellRow, col];
                            stairesMatrix[notDestroyedCellRow, col] = ' ';

                            row--;
                            notDestroyedCellRow--;
                        }


                        break;
                    }

                    row--;
                }
            }
        }

        private static void PrintStairsMatrixAfterDestruction(char[,] stairesMatrix)
        {
            for (int row = 0; row < stairesMatrix.GetLength(0); row++)
            {
                for (int col = 0; col < stairesMatrix.GetLength(1); col++)
                {
                    Console.Write(stairesMatrix[row, col]);
                }

                Console.WriteLine();
            }
        }

    }
}
