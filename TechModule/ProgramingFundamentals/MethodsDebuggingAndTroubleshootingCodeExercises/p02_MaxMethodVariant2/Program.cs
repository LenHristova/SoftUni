using System;
using System.Linq;

namespace p02_MaxMethodVariant2
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] numbers = ReadThreeNumbers();
            Console.WriteLine(numbers.Max());
        }

        private static int[] ReadThreeNumbers()
        {
            int[] numbers = new int[3];
            for (int i = 0; i < 3; i++)
            {
                numbers[i] = int.Parse(Console.ReadLine());
            }
            return numbers;
        }
    }
}
