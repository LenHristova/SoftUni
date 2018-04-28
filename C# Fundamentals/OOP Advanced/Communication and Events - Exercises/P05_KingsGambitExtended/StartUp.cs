using System;

using P05_KingsGambitExtended.Contracts;
using P05_KingsGambitExtended.Models;

namespace P05_KingsGambitExtended
{
    class StartUp
    {
        static void Main()
        {
            var king = SetUpKing();
            var engine = new Engine(king);
            engine.Run();
        }

        private static IKing SetUpKing()
        {
            var kingName = Console.ReadLine();
            var king = new King(kingName);
            var royalGardsNames = Console.ReadLine()?.Split();
            foreach (var name in royalGardsNames)
            {
                king.AddSubordinate(new RoyalGuard(name));
            }

            var footmenNames = Console.ReadLine()?.Split();
            foreach (var name in footmenNames)
            {
                king.AddSubordinate(new Footman(name));
            }

            return king;
        }
    }
}
