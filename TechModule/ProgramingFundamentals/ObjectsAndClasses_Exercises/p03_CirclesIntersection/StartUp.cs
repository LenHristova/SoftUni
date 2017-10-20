using System;
using System.Collections.Generic;
using System.Linq;

class Point
{
    public double X { get; set; }
    public double Y { get; set; }
}

class Circle
{
    public Point Center { get; set; }
    public double Radius { get; set; }
}
class StartUp
{
    static void Main()
    {
        Circle c1 = GetParams();
        Circle c2 = GetParams();

        Console.WriteLine(HasIntersection(c1, c2) ? "Yes" : "No");
    }

    public static bool HasIntersection(Circle circle1, Circle circle2)
    {
        double distanceBtwCenters = Math.Sqrt(
            Math.Pow(circle1.Center.X - circle2.Center.X, 2) +
            Math.Pow(circle1.Center.Y - circle2.Center.Y, 2));
        return circle1.Radius + circle2.Radius >= distanceBtwCenters;
    }


    private static Circle GetParams()
    {
        string[] circleParams = Console.ReadLine()
            .Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);

        return new Circle
        {
            Center = new Point
            {
                X = Double.Parse(circleParams[0]),
                Y = Double.Parse(circleParams[1])
            },
            Radius = Double.Parse(circleParams[2])
        };
    }
}