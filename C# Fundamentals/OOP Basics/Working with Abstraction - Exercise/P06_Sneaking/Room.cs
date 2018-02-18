using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace P06_Sneaking
{
     static class  Room
    {
        public static char[][] Matrix { get; set; }
        public static List<Enemy> Enemies { get; set; } = new List<Enemy>();
        public static Sam Sam { get; set; }
        public static Nikoladze Nikoladze { get; set; }

        public static void InicializeRoom()
        {
            var n = int.Parse(Console.ReadLine());
            Matrix = new char[n][];
            for (int row = 0; row < n; row++)
            {
                var input = Console.ReadLine()?.ToCharArray();
                if (input != null)
                {
                    Matrix[row] = new char[input.Length];
                    for (int col = 0; col < input.Length; col++)
                    {
                        var cellValue = input[col];
                        //Fills info for cell
                        Matrix[row][col] = cellValue;

                        //Looking for Sam in that cell
                        if (cellValue == 'S')
                        {
                            Sam = new Sam(row, col);
                        }

                        if (cellValue == 'N')
                        {
                            Nikoladze = new Nikoladze(row, col);
                        }

                        if (cellValue == 'b')
                        {
                            Enemies.Add(new Enemy(row, col, "right"));
                        }

                        if (cellValue == 'd')
                        {
                            Enemies.Add(new Enemy(row, col, "left"));
                        }
                    }
                }
            }
        }       

        public static Enemy GetEnemyPosition()
        {
            return Enemies.FirstOrDefault(x => x.RowPosition == Sam.RowPosition);
        }

        public static void RemoveHero(Hero hero)
        {
            Matrix[hero.RowPosition][hero.ColPosition] = 'X';
        }
        public static void Print()
        {
            var sb = new StringBuilder();
            foreach (var rowValue in Matrix)
            {
                foreach (var colValue in rowValue)
                {
                    sb.Append(colValue);
                }

                sb.AppendLine();
            }

            Console.WriteLine(sb.ToString());
        }
    }
}
