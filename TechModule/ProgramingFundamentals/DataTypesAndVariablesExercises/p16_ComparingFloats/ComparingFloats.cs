using System;

namespace p16_ComparingFloats
{
    class ComparingFloats
    {
        static void Main(string[] args)
        {
            double num1 = double.Parse(Console.ReadLine());
            double num2 = double.Parse(Console.ReadLine());

            double diff = Math.Abs(num1 - num2);
            double eps = 0.000001;
            bool isEqual = diff < eps;
            Console.WriteLine(isEqual);
        }
    }
}
