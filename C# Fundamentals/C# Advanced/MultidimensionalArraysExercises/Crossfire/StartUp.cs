using System;
using System.Collections.Generic;
using System.Linq;

namespace Crossfire
{
    class StartUp
    {
        private static List<List<long>> _jagged;
        private static bool _hasChange;

        static void Main()
        {
            var size = Console.ReadLine()
                .Split(new[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            FillJagged(size);

            while (true)
            {
                string input = Console.ReadLine();
                if (input == "Nuke it from orbit")
                    break;

                var commandArgs = input
                    .Split(new[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                DestroyCells(commandArgs);

                if (_hasChange)
                {
                    Rearange();
                }
            }

            foreach (var row in _jagged)
            {
                Console.WriteLine(string.Join(" ", row));
            }
        }

        private static void Rearange()
        {
            for (int row = 0; row < _jagged.Count; row++)
            {
                _jagged[row] = _jagged[row].Where(n => n != 0).ToList();
            }

            _jagged = _jagged.Where(list => list.Count > 0).ToList();
            _hasChange = false;
        }

        private static void DestroyCells(int[] commandArgs)
        {
            var attackedRow = commandArgs[0];
            var attackedCol = commandArgs[1];
            var radius = commandArgs[2];

            if (attackedCol >= 0)
            {
                var startRow = (long)attackedRow - radius;
                if (startRow < _jagged.Count)
                {
                    startRow = Math.Max(0, startRow);
                    var endRow = (int)Math.Min(_jagged.Count - 1, (long)attackedRow + radius);

                    for (int row = (int)startRow; row <= endRow; row++)
                    {
                        if (attackedCol < _jagged[row].Count)
                        {
                            _jagged[row][attackedCol] = 0;
                            _hasChange = true;
                        }
                    }
                }
            }

            if (attackedRow >= 0 && attackedRow < _jagged.Count)
            {
                var startCol = (long)attackedCol - radius;
                if (startCol < _jagged[attackedRow].Count)
                {
                    startCol = Math.Max(0, startCol);
                    var endCol = (int)Math.Min(_jagged[attackedRow].Count - 1, (long)attackedCol + radius);

                    for (int col = (int)startCol; col <= endCol; col++)
                    {
                        _jagged[attackedRow][col] = 0;
                        _hasChange = true;
                    }
                }
            }
        }

        private static void FillJagged(int[] size)
        {
            var rows = size[0];
            var cols = size[1];

            _jagged = new List<List<long>>();

            for (int row = 0; row < rows; row++)
            {
                _jagged.Add(new List<long>());
                for (int col = 1; col <= cols; col++)
                {
                    var num = (long)row * cols + col;

                    _jagged[row].Add(num);
                }
            }
        }
    }
}
