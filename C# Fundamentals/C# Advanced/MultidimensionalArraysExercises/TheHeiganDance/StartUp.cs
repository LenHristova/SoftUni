using System;
using System.Linq;

namespace TheHeiganDance
{
    class StartUp
    {
        private static readonly string[][] Chamber = new string[15][]
                                                .Select(arr => new string[15])
                                                .ToArray();
        private static int[] _playerPosition = { 7, 7 };

        static void Main()
        {

            var playerPoints = 18500M;
            var heiganPoints = 3000000M;
            var heiganDamagebyTurn = decimal.Parse(Console.ReadLine());

            int[] cloud = null;
            string spell;

            while (true)
            {
                heiganPoints -= heiganDamagebyTurn;

                var input = Console.ReadLine()
                    .Split(new[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                spell = input[0];
                var spellRow = int.Parse(input[1]);
                var spellCol = int.Parse(input[2]);

                var isCatchedPlayer = false;
                if (heiganPoints > 0)
                {
                    isCatchedPlayer = SpreadSpell(spellRow, spellCol);
                }

                var isPlayerDamaged = false;
                if (isCatchedPlayer)
                {
                    isPlayerDamaged = TryToEscape();
                }

                if (cloud != null)
                {
                    playerPoints -= 3500;
                    cloud = null;
                }

                if (playerPoints < 0)
                {
                    spell = "Plague Cloud";
                }

                if (isPlayerDamaged)
                {
                    switch (spell)
                    {
                        case "Cloud":
                            playerPoints -= 3500;
                            break;
                        case "Eruption":
                            playerPoints -= 6000;
                            break;
                    }
                }

                if (spell == "Cloud" && isPlayerDamaged)
                {
                    spell = "Plague Cloud";
                    cloud = new[] { spellRow, spellCol };
                }

                RemoveSpell(spellRow, spellCol);

                if (playerPoints <= 0 || heiganPoints <= 0)
                {
                    break;
                }
            }

            Console.WriteLine(heiganPoints <= 0
                ? "Heigan: Defeated!"
                : $"Heigan: {heiganPoints:F2}");
            Console.WriteLine(playerPoints <= 0
                ? $"Player: Killed by {spell}"
                : $"Player: {playerPoints}");
            Console.WriteLine($"Final position: {_playerPosition[0]}, {_playerPosition[1]}");
        }

        private static void RemoveSpell(int spellRow, int spellCol)
        {
            for (int row = spellRow - 1; row <= spellRow + 1; row++)
            {
                for (int col = spellCol - 1; col <= spellCol + 1; col++)
                {
                    if (!IsInChamber(row, col))
                    {
                        continue;
                    }

                    Chamber[row][col] = string.Empty;
                }
            }
        }

        private static bool TryToEscape()
        {
            var playerRow = _playerPosition[0];
            var playerCol = _playerPosition[1];
            var playersNewPositions = new[]
            {
                    new[] {playerRow - 1, playerCol},
                    new[] {playerRow, playerCol + 1},
                    new[] {playerRow + 1, playerCol},
                    new[] {playerRow, playerCol - 1}
            };

            foreach (var playerNewPosition in playersNewPositions)
            {
                var newRow = playerNewPosition[0];
                var newCol = playerNewPosition[1];
                if (IsInChamber(newRow, newCol) && Chamber[newRow][newCol] != "Spell")
                {
                    _playerPosition = new[] { newRow, newCol };
                    return false;
                }
            }

            return true;
        }

        private static bool SpreadSpell(int spellRow, int spellCol)
        {
            var isCatchedPlayer = false;
            for (int row = spellRow - 1; row <= spellRow + 1; row++)
            {
                for (int col = spellCol - 1; col <= spellCol + 1; col++)
                {
                    if (!IsInChamber(row, col))
                    {
                        continue;
                    }

                    if (row == _playerPosition[0] && col == _playerPosition[1])
                    {
                        isCatchedPlayer = true;
                    }

                    Chamber[row][col] = "Spell";
                }
            }

            return isCatchedPlayer;
        }

        private static bool IsInChamber(int row, int col)
        {
            return row >= 0 && row < Chamber.Length &&
                   col >= 0 && col < Chamber[0].Length;
        }
    }
}
