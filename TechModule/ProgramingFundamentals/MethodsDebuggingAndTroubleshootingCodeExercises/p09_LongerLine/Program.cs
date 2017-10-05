using System;

namespace p09_LongerLine
{
    class Program
    {
        static void Main(string[] args)
        {
            double x1 = double.Parse(Console.ReadLine());
            double y1 = double.Parse(Console.ReadLine());
            double x2 = double.Parse(Console.ReadLine());
            double y2 = double.Parse(Console.ReadLine());

            double x3 = double.Parse(Console.ReadLine());
            double y3 = double.Parse(Console.ReadLine());
            double x4 = double.Parse(Console.ReadLine());
            double y4 = double.Parse(Console.ReadLine());

            PrintLongerLine(x1, y1, x2, y2, x3, y3, x4, y4);
        }

        //Print coordinates of the line that is longer
        static void PrintLongerLine(
            double x1, double y1, double x2, double y2,
            double x3, double y3, double x4, double y4)
        {
            double firstLineLenght = Math.Sqrt(
                Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
            double secondLineLenght = Math.Sqrt(
                Math.Pow(x4 - x3, 2) + Math.Pow(y4 - y3, 2));
            string startPoint = String.Empty;
            string endPoint = String.Empty;
            if (firstLineLenght >= secondLineLenght)
            {
                startPoint = FindClosestToTheCenterPoint(x1, y1, x2, y2);
                endPoint = FindMoreDistantToTheCenterPoint(x1, y1, x2, y2);
            }
            else
            {
                startPoint = FindClosestToTheCenterPoint(x3, y3, x4, y4);
                endPoint = FindMoreDistantToTheCenterPoint(x3, y3, x4, y4);
            }
            Console.WriteLine(startPoint + endPoint);
        }

        static string FindMoreDistantToTheCenterPoint(
            double x1, double y1, double x2, double y2)
        {
            double coordinatesX1Y1 = Math.Abs(x1) + Math.Abs(y1);
            double coordinatesX2Y2 = Math.Abs(x2) + Math.Abs(y2);
            string distantPoint = String.Empty;
            if (coordinatesX1Y1 <= coordinatesX2Y2)
            {
                distantPoint = $"({x2}, {y2})";
            }
            else
            {
                distantPoint = $"({x1}, {y1})";
            }
            return distantPoint;
        }

        static string FindClosestToTheCenterPoint(
            double x1, double y1, double x2, double y2)
        {
            double coordinatesX1Y1 = Math.Abs(x1) + Math.Abs(y1);
            double coordinatesX2Y2 = Math.Abs(x2) + Math.Abs(y2);
            string closestPoint = String.Empty;
            if (coordinatesX1Y1 <= coordinatesX2Y2)
            {
                closestPoint = $"({x1}, {y1})";
            }
            else
            {
                closestPoint = $"({x2}, {y2})";
            }
            return closestPoint;
        }
    }
}

