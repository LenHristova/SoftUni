using System;
using System.Linq;

namespace P03_JediGalaxy
{
    class StartUp
    {
        public static int[,] matrix;
        public static int rows;
        public static int cols;

        static void Main()
        {
            matrix = GetMatrix();

            string input;
            long sum = 0;
            while ((input = Console.ReadLine()) != "Let the Force be with you")
            {
                Ivo ivo = new Ivo(input);
                Evil evil = new Evil(Console.ReadLine());

                Destroy(evil);
                sum = GetStars(sum, ivo);
            }

            Console.WriteLine(sum);
        }

        private static long GetStars(long sum, Ivo ivo)
        {
            while (ivo.RowPosition >= 0 && ivo.ColPosition < cols)
            {
                if (ivo.RowPosition < rows && ivo.ColPosition >= 0)
                {
                    sum += matrix[ivo.RowPosition, ivo.ColPosition];
                }

                ivo.Move();
            }

            return sum;
        }

        private static void Destroy(Evil evil)
        {
            while (evil.RowPosition >= 0 && evil.ColPosition >= 0)
            {
                if (evil.RowPosition < rows && evil.ColPosition < cols)
                {
                    matrix[evil.RowPosition, evil.ColPosition] = 0;
                }

                evil.Move();
            }
        }

        private static int[,] GetMatrix()
        {
            int[] dimestions = Console.ReadLine()
                .Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            rows = dimestions[0];
            cols = dimestions[1];

            matrix = new int[rows, cols];

            int value = 0;
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    matrix[row, col] = value++;
                }
            }

            return matrix;
        }
    }
}
