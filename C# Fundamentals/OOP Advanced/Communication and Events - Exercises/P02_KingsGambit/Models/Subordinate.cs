using System;

using P02_KingsGambit.Contracts;

namespace P02_KingsGambit.Models
{
    public abstract class Subordinate : ISubordinate
    {
        private readonly string _reactionToAttack;

        protected Subordinate(string name, string reactionToAttack)
        {
            Name = name;
            _reactionToAttack = reactionToAttack;
            IsAlive = true;
        }

        public string Name { get; }

        public bool IsAlive { get; private set; }

        public void BeKilled()
        {
            IsAlive = false;
        }

        public void ReactToAttack()
        {
            if (IsAlive)
            {
                Console.WriteLine(GetReaction());
            }
        }

        protected virtual string GetReaction()
        {
            return $"{GetType().Name} {Name} is {_reactionToAttack}!";
        }
    }
}
