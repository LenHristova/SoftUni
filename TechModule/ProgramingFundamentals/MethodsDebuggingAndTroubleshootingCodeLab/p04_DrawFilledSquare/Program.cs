using System;
using System.Text;

namespace p04_DrawFilledSquare
{
    class Program
    {
        static void Main(string[] args)
        {
            int height = int.Parse(Console.ReadLine());
            int width = height * 2;

            PrintHeaderFooter(width);
            PrintMiddleRows(height);
            PrintHeaderFooter(width);
        }

        private static void PrintHeaderFooter(int width)
        {
            Console.WriteLine(new String('-', width));
        }

        private static void PrintMiddleRows(int height)
        {
            for (int row = 0; row < height - 2; row++)
            {
                Console.WriteLine(
                    $"-" +
                    $"{new StringBuilder().Insert(0, "\\/", height-1).ToString()}" +
                    $"-");
            }
        }
    }
}
