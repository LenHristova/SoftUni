using System;
using System.Linq;

using P02_KingsGambit.Contracts;

namespace P02_KingsGambit
{
    public class Engine
    {
        private readonly IKing _king;

        public Engine(IKing king)
        {
            _king = king;
        }

        public void Run()
        {
            string input;
            while ((input = Console.ReadLine()) != "End")
            {
                var commandArgs = input?.Split();
                var command = commandArgs?[0];
                switch (command)
                {
                    case "Attack":
                        _king.BeAttacked();
                        break;
                    case "Kill":
                        var killedName = commandArgs[1];
                        var subordinate = _king.Subordinates
                            .FirstOrDefault(f => f.Name == killedName);
                        subordinate?.BeKilled();
                        break;
                }
            }
        }
    }
}