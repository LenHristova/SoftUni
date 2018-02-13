using System;
using System.Collections.Generic;
using System.Linq;

namespace ParkingSystem
{
    class StartUp
    {
        static void Main()
        {
            var size = Console.ReadLine()
                .Split(new[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var rows = size[0];
            var cols = size[1];

            var parkingArea = GetArea(rows);

            while (true)
            {
                var input = Console.ReadLine();
                if (input == "stop") break;

                var parkingParams = input
                    .Split(new[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                var entryRow = parkingParams[0];
                var row = parkingParams[1];
                var col = parkingParams[2];

                var desireRow = parkingArea[row];

                if (desireRow.Count == cols - 1)
                {
                    Console.WriteLine($"Row {row} full");
                    continue;
                }

                if (desireRow.Contains(col))
                {
                    var nextLeftCol = col - 1;
                    var nextRightCol = col + 1;
                    while (nextLeftCol > 0 || nextRightCol < cols)
                    {
                        if (nextLeftCol > 0 &&!desireRow.Contains(nextLeftCol))
                        {
                            col= nextLeftCol;
                            break;
                        }

                        if (nextRightCol < cols && !desireRow.Contains(nextRightCol))
                        {
                            col = nextRightCol;
                            break;
                        }

                        nextLeftCol--;
                        nextRightCol++;
                    }
                }
                parkingArea[row].Add(col);
                var stepsToTheDesireRow = Math.Abs(row - entryRow);
                Console.WriteLine(stepsToTheDesireRow + col + 1);
            }
        }

        private static List<HashSet<int>> GetArea(int rows)
        {
            var parkingArea = new List<HashSet<int>>();

            for (int row = 0; row < rows; row++)
            {
                parkingArea.Add(new HashSet<int>());
            }
            return parkingArea;
        }
    }
}
