using System;

namespace p09_LongerLineVariant2
{
    class Program
    {
        static void Main(string[] args)
        {
            double[][] line1 = GetCoordinates();
            double[][] line2 = GetCoordinates();

            PrintLongerLine(line1, line2);
        }

        static void PrintLongerLine(double[][] line1, double[][] line2)
        {
            if (GetLenght(line1) >= GetLenght(line2))
            {
                if (GetDistanceToTheCenter(line1[0]) <= GetDistanceToTheCenter(line1[1]))
                {
                    Console.WriteLine($"({string.Join(", ", line1[0])})({string.Join(", ", line1[1])})");
                }
                else
                {
                    Console.WriteLine($"({string.Join(", ", line1[1])})({string.Join(", ", line1[0])})");
                }
            }
            else
            {
                if (GetDistanceToTheCenter(line2[0]) <= GetDistanceToTheCenter(line2[1]))
                {
                    Console.WriteLine($"({string.Join(", ", line2[0])})({string.Join(", ", line2[1])})");
                }
                else
                {
                    Console.WriteLine($"({string.Join(", ", line2[1])})({string.Join(", ", line2[0])})");
                }
            }
        }

        private static double GetDistanceToTheCenter(double[] point)
        {
            return Math.Abs(point[0]) + Math.Abs(point[1]);
        }

        private static double GetLenght(double[][] line)
        {
            double x1 = line[0][0];
            double y1 = line[0][1];
            double x2 = line[1][0];
            double y2 = line[1][1];
            return Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));
        }

        static double[][] GetCoordinates()
        {
            return new double[2][]
                { new double[2]
                    {
                    double.Parse(Console.ReadLine()),
                    double.Parse(Console.ReadLine())
                    },
                  new double[2] {
                    double.Parse(Console.ReadLine()),
                    double.Parse(Console.ReadLine())
                    }
                };
        }
    }
}