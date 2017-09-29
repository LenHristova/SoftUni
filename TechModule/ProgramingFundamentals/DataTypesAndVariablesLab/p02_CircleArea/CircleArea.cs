using System;

namespace p02_CircleArea
{
    class CircleArea
    {
        static void Main(string[] args)
        {
            double radius = double.Parse(Console.ReadLine());

            double area = Math.PI * radius * radius;
            Console.WriteLine($"{area:f12}");
        }
    }
}
