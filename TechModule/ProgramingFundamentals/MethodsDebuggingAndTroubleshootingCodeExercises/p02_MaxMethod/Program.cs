using System;

namespace p02_MaxMethod
{
    class Program
    {
        static void Main(string[] args)
        {
            int num1 = int.Parse(Console.ReadLine());
            int num2 = int.Parse(Console.ReadLine());
            int num3 = int.Parse(Console.ReadLine());
            int max = GetMax(GetMax(num1, num2), num3);
            Console.WriteLine(max);
        }

        private static int GetMax(int num1, int num2)
        {
            return num1 >= num2 ? num1 : num2;
        }
    }
}
