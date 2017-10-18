using System;
using System.Collections.Generic;
using System.Linq;

class StartUp
{
    public class Point
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Point GetCoordinates()
        {
            int[] coordinates = Console.ReadLine()
                .Split(' ')
                .Select(int.Parse)
                .ToArray();
            return new Point
            {
                X = coordinates[0],
                Y = coordinates[1]
            };
        }
    }

    public class Line
    {
        public Point P1 { get; set; }
        public Point P2 { get; set; }
        public double Distance =>
            Math.Sqrt(Math.Pow(P1.X - P2.X, 2) + Math.Pow(P1.Y - P2.Y, 2));

    }

    static void Main()
    {
        int pointsCount = int.Parse(Console.ReadLine());
        Point[] allPoints = new Point[pointsCount];

        for (int pos = 0; pos < pointsCount; pos++)
        {
            allPoints[pos] = new Point().GetCoordinates();
        }

        List<Line> allLines = new List<Line>();

        for (int pos1 = 0; pos1 < allPoints.Length; pos1++)
        {
            for (int pos2 = pos1 + 1; pos2 < allPoints.Length; pos2++)
            {
                allLines.Add(new Line
                {
                    P1 = allPoints[pos1],
                    P2 = allPoints[pos2]
                });
            }
        }

        allLines = allLines.OrderBy(l => l.Distance).ToList();

        Console.WriteLine($"{allLines[0].Distance:F3}");
        Console.WriteLine($"({allLines[0].P1.X}, {allLines[0].P1.Y})");
        Console.WriteLine($"({allLines[0].P2.X}, {allLines[0].P2.Y})");
    }
}