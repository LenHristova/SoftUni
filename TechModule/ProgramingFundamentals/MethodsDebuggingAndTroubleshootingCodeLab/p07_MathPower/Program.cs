using System;

namespace p07_MathPower
{
    class Program
    {
        static void Main(string[] args)
        {
            double number = double.Parse(Console.ReadLine());
            int power = int.Parse(Console.ReadLine());
            double poweredNumber = RaiseToPower(number, power);
            Console.WriteLine(poweredNumber);
        }

        private static double RaiseToPower(double number, int power)
        {
            return Math.Pow(number, power);
        }
    }
}
