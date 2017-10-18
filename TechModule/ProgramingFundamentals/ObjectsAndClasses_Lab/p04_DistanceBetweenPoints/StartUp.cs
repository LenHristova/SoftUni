using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

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
        Line line = new Line
        {
            P1 = new Point().GetCoordinates(),
            P2 = new Point().GetCoordinates()
        };

        Console.WriteLine(line.Distance);
    }
}