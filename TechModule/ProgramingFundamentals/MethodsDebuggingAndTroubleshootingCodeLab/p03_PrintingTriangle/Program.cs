using System;
using System.Text;

namespace p03_PrintingTriangle
{
    class Program
    {
        static void Main(string[] args)
        {
            int num = int.Parse(Console.ReadLine());

            PrintTriangle(num);
        }

        private static void PrintTriangle(int num)
        {
            for (int topRow = 1; topRow < num; topRow++)
            {
                PrintRowsContent(topRow);
            }
            for (int bottomRow = num; bottomRow > 0; bottomRow--)
            {
                PrintRowsContent(bottomRow);
            }
        }

        private static void PrintRowsContent(int row)
        {
            StringBuilder rowsContent = new StringBuilder("");
            for (int col = 1; col <= row; col++)
            {
                rowsContent.Append($"{col} ");
            }
            Console.WriteLine(rowsContent);
        }
    }
}
