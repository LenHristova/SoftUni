using System;
using System.Collections.Generic;
using System.Linq;

using P02_KingsGambit.Contracts;
using P02_KingsGambit.Models;

namespace P02_KingsGambit
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

            IEnumerable<ISubordinate> royalGards = Console.ReadLine()?
                 .Split()
                 .Select(guardName => new RoyalGuard(guardName))
                 .ToList();

            IEnumerable<ISubordinate> footmen = Console.ReadLine()?
                 .Split()
                 .Select(footmenName => new Footman(footmenName))
                 .ToList();

            return new King(kingName, royalGards?.Concat(footmen));
        }
    }
}
