using System;

namespace P06_Sneaking
{
    class Sneaking
    {
        static void Main()
        {
            Room.InicializeRoom();
            var sam = Room.Sam;
            var nikoladze = Room.Nikoladze;
            var enemies = Room.Enemies;

            var moves = Console.ReadLine()?.ToCharArray();
            if (moves != null)
                foreach (var move in moves)
                {
                    foreach (var enemy in enemies)
                    {
                        enemy.Move(enemy.Direction);
                    }

                    var enemyInSamRow = Room.GetEnemyPosition();
                    if (sam.IsCought(enemyInSamRow))
                    {
                        Room.RemoveHero(sam);
                        Console.WriteLine($"Sam died at {sam.RowPosition}, {sam.ColPosition}");
                        break;
                    }

                    sam.Move(move.ToString());

                    if (sam.RowPosition == nikoladze.RowPosition)
                    {
                        Room.RemoveHero(nikoladze);
                        Console.WriteLine("Nikoladze killed!");
                        break;
                    }
                }

            Room.Print();
        }
    }
}
