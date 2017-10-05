using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace p10_10.CubeProperties
{
    class Program
    {
        static void Main(string[] args)
        {
            double cubeSide = double.Parse(Console.ReadLine());
            string cubeParameter = Console.ReadLine().ToLower();
            PrintParameterResult(cubeSide, cubeParameter);
        }

        static void PrintParameterResult(double side, string parameter)
        {
            double result = 0.0;
            switch (parameter)
            {
                case "face":
                    result = CalcFace(side);
                    break;
                case "space":
                    result = CalcSpace(side);
                    break;
                case "volume":
                    result = CalcVolume(side);
                    break;
                case "area":
                    result = CalcArea(side);
                    break;
            }
            Console.WriteLine($"{result:F2}");
        }

        static double CalcArea(double side)
        {
            double area = Math.Pow(side, 2) * 6;
            return area;
        }

        static double CalcVolume(double side)
        {
            double volume = Math.Pow(side, 3);
            return volume;
        }

        static double CalcSpace(double side)
        {
            double spaceDiagonals = Math.Sqrt(Math.Pow(side, 2) * 3);
            return spaceDiagonals;
        }

        static double CalcFace(double side)
        {
            double faceDiagonals = Math.Sqrt(Math.Pow(side, 2) * 2);
            return faceDiagonals;
        }
    }
}
