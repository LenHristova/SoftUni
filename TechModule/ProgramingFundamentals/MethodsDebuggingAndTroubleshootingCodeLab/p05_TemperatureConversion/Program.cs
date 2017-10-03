using System;

namespace p05_TemperatureConversion
{
    class Program
    {
        static void Main(string[] args)
        {
            double fahrenheit = double.Parse(Console.ReadLine());
            double celsius = ConvertToCelsius(fahrenheit);
            Console.WriteLine($"{celsius:F2}");
        }

        private static double ConvertToCelsius(double fahrenheit)
        {
            return (fahrenheit - 32) * 5 / 9;
        }
    }
}
