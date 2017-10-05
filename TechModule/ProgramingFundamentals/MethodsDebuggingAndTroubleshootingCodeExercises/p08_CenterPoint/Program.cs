using System;

namespace p08_CenterPoint
{
    class Program
    {
        static void Main(string[] args)
        {
            double[] point1 = GetCoordinates();
            double[] point2 = GetCoordinates();
            PrintClosestToTheCenterPoint(point1, point2);

        }

        static void PrintClosestToTheCenterPoint(double[] point1, double[] point2)
        {
            if (GetDistanceToTheCenter(point1) <= GetDistanceToTheCenter(point2))
            {
                Console.WriteLine($"({string.Join(", ", point1)})");
            }
            else
            {
                Console.WriteLine($"({string.Join(", ", point2)})");
            }         
        }

        private static double GetDistanceToTheCenter(double[] point)
        {
            return Math.Abs(point[0]) + Math.Abs(point[1]);
        }

        static double[] GetCoordinates()
        {
            return new double[2]
                {
                    double.Parse(Console.ReadLine()),
                    double.Parse(Console.ReadLine())
                };
        }
    }
}
