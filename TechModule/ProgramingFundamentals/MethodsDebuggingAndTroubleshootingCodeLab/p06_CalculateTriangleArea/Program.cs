using System;

namespace p06_CalculateTriangleArea
{
    class Program
    {
        static void Main(string[] args)
        {
            double triangleBase = double.Parse(Console.ReadLine());
            double triangleHeight = double.Parse(Console.ReadLine());
            double triangleArea = GetTriangleArea(triangleBase, triangleHeight);
            Console.WriteLine(triangleArea);
        }

        private static double GetTriangleArea(double triangleBase, double triangleHeight)
        {
            return triangleBase * triangleHeight / 2;
        }
    }
}
