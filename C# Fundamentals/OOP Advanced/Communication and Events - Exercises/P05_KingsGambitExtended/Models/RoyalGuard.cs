using System;

namespace P05_KingsGambitExtended.Models
{
    public class RoyalGuard : Subordinate
    {
        private const string REACTION_TO_ATTACK = "defending";
        private const int HIT_POINTS = 3;

        public RoyalGuard(string name) :
            base(name, REACTION_TO_ATTACK, HIT_POINTS)
        {
        }

        public override void ReactToAttack()
        {
            Console.WriteLine($"Royal Guard {Name} is {_reactionToAttack}!");
        }
    }
}
