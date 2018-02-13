using System;
using System.Collections.Generic;
using System.Linq;

namespace RadioactiveMutantVampireBunnies
{
    class Program
    {
        private static char[][] _lair;

        private static int[] _playerPosition;

        private static Queue<int[]> _bunniesPositions;

        public static bool IsAlivePlayer { get; set; } = true;

        public static bool IsFindExit { get; set; }

        static void Main()
        {
            var input = Console.ReadLine()
                .Split(new[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var rows = input[0];
            var cols = input[1];

            _lair = GetMatrix(rows, cols);

            GetPlayerAndBunniesPositions();

            if (_playerPosition == null)
            {
                return;
            }

            var directions = Console.ReadLine();

            foreach (var direction in directions)
            {
                Moving(direction);

                var currentBunniesCount = _bunniesPositions.Count;
                for (int pos = 0; pos < currentBunniesCount; pos++)
                {
                    SpreadBunnie(_bunniesPositions.Dequeue());
                }

                if (IsFindExit || !IsAlivePlayer)
                {
                    break;
                }
            }

            foreach (var row in _lair)
            {
                Console.WriteLine(string.Join("", row));
            }

            Console.WriteLine(IsAlivePlayer
                ? $"won: {_playerPosition[0]} {_playerPosition[1]}"
                : $"dead: {_playerPosition[0]} {_playerPosition[1]}");
        }


        private static void SpreadBunnie(int[] bunnie)
        {
            int row = bunnie[0];
            int col = bunnie[1];

            var newBunniesPositions = new int[4][]
            {
                new int[] {row, col - 1},
                new int[] {row, col + 1},
                new int[] {row - 1, col},
                new int[] {row + 1, col},
            };

            foreach (var newBunniePosition in newBunniesPositions)
            {
                if (IsOutOfTheLair(newBunniePosition[0], newBunniePosition[1]))
                {
                   continue;
                }

                if (_lair[newBunniePosition[0]][newBunniePosition[1]] == 'P')
                {
                    IsAlivePlayer = false;
                }

                if (_lair[newBunniePosition[0]][newBunniePosition[1]] != 'B')
                {
                    _lair[newBunniePosition[0]][newBunniePosition[1]] = 'B';
                    _bunniesPositions.Enqueue(new int[]{newBunniePosition[0], newBunniePosition[1]});
                }
            }
        }

        private static void Moving(char direction)
        {
            var row = _playerPosition[0];
            var col = _playerPosition[1];
            switch (direction)
            {
                case 'R':
                    col++;
                    break;
                case 'L':
                    col--;
                    break;
                case 'U':
                    row--;
                    break;
                case 'D':
                    row++;
                    break;
            }

            _lair[_playerPosition[0]][_playerPosition[1]] = '.';

            if (IsOutOfTheLair(row, col))
            {
                IsFindExit = true;
                return;
            }

            var playerNextStep = _lair[row][col];
            _playerPosition[0] = row;
            _playerPosition[1] = col;
            if (playerNextStep == 'B')
            {
                IsAlivePlayer = false;
            }
            else
            {
                _lair[row][col] = 'P';
            }

        }

        private static bool IsOutOfTheLair(int row, int col)
        {
            return row < 0 ||
                   row >= _lair.Length ||
                   col < 0 ||
                   col >= _lair[0].Length;
        }

        private static void GetPlayerAndBunniesPositions()
        {
            _playerPosition = new int[2];
            _bunniesPositions = new Queue<int[]>();

            for (int row = 0; row < _lair.Length; row++)
            {
                for (int col = 0; col < _lair[row].Length; col++)
                {
                    var currentCellValue = _lair[row][col];
                    switch (currentCellValue)
                    {
                        case 'P':
                            _playerPosition = new int[] { row, col };
                            break;
                        case 'B':
                            _bunniesPositions.Enqueue(new int[] { row, col });
                            break;
                    }
                }
            }
        }

        private static char[][] GetMatrix(int rows, int cols)
        {
            var matrix = new char[rows][];

            for (int row = 0; row < rows; row++)
            {
                matrix[row] = Console.ReadLine().ToCharArray();
            }

            return matrix;
        }
    }
}
