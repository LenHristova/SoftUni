using System;
using System.Collections.Generic;
using System.Linq;

namespace KnightGame
{
    class StartUp
    {
        private static char[][] board;
        private static List<Knight> atackers;

        static void Main()
        {
            var size = int.Parse(Console.ReadLine());
            board = new char[size][];
            FillBoard();

            atackers = new List<Knight>();
            GetAttackers();

            var removedKnights = 0;
            while (true)
            {
                if (atackers.Count == 0)
                {
                    break;
                }

                atackers = atackers
                    .OrderByDescending(k => k.PossibleTargets)
                    .ToList();

                board[atackers[0].RowPos][atackers[0].ColPos] = '0';
                atackers.RemoveAt(0);
                removedKnights++;
                GetAttackers();
            }

            Console.WriteLine(removedKnights);
        }

        private static int FindPossibleTargetsCount(int row, int col)
        {
            var knightSteps = new[]
            {
                new[] {row - 2, col - 1},
                new[] {row - 2, col + 1},
                new[] {row - 1, col + 2},
                new[] {row + 1, col + 2},
                new[] {row + 2, col - 1},
                new[] {row + 2, col + 1},
                new[] {row + 1, col - 2},
                new[] {row - 1, col - 2},
            };

            var targetsCount = 0;
            foreach (var knightStep in knightSteps)
            {
                var newRow = knightStep[0];
                var newCol = knightStep[1];

                if (IsCellInMatrix(newRow, newCol) &&
                    board[newRow][newCol] == 'K')
                {
                    targetsCount++;
                }
            }

            return targetsCount;
        }

        private static void GetAttackers()
        {
            var currentAttackersList = new List<Knight>();
            for (int row = 0; row < board.Length; row++)
            {
                for (int col = 0; col < board.Length; col++)
                {
                    if (board[row][col] == 'K')
                    {
                        var possibleTargets = FindPossibleTargetsCount(row, col);
                        if (possibleTargets > 0)
                        {
                            currentAttackersList.Add(new Knight(row, col, possibleTargets));
                        }
                    }
                }
            }

            atackers = currentAttackersList;
        }

        private static void FillBoard()
        {
            for (int row = 0; row < board.Length; row++)
            {
                board[row] = Console.ReadLine()
                    .ToCharArray();
            }
        }

        public static bool IsCellInMatrix(int row, int col)
        {
            return 0 <= row && row < board.Length
                 && 0 <= col && col < board[row].Length;
        }
    }
}
