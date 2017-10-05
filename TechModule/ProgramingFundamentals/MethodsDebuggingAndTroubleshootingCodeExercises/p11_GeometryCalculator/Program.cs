using System;

namespace p11_GeometryCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            string figureType = Console.ReadLine().ToLower();
            double area = CalcArea(figureType);
            Console.WriteLine($"{area:F2}");
        }

        private static double CalcArea(string figureType)
        {
            if (figureType == "triangle")
            {
                double side = double.Parse(Console.ReadLine());
                double height = double.Parse(Console.ReadLine());
                return side * height / 2;
            }
            else if (figureType == "square")
            {
                double side = double.Parse(Console.ReadLine());
                return Math.Pow(side, 2);
            }
            else if (figureType == "rectangle")
            {
                double width = double.Parse(Console.ReadLine());
                double height = double.Parse(Console.ReadLine());
                return width * height;
            }
            double radius = double.Parse(Console.ReadLine());
            return Math.PI * Math.Pow(radius, 2);
        }
    }
}
