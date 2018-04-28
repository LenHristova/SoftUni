using System;

using P05_KingsGambitExtended.Contracts;

namespace P05_KingsGambitExtended.Models
{
    public abstract class Subordinate : ISubordinate
    {
        protected readonly string _reactionToAttack;

        protected Subordinate(string name, string reactionToAttack, int hitPoints)
        {
            Name = name;
            _reactionToAttack = reactionToAttack;
            HitPoints = hitPoints;
            IsAlive = true;
        }

        public string Name { get; }

        public int HitPoints { get; private set; }

        public bool IsAlive { get; private set; }

        public void TakeDamage()
        {
            HitPoints--;
            IsAlive = false;

            if (HitPoints == 0)
            {
                DeathEvent?.Invoke(this);
            }
        }

        public event SubordinateDeathEventHandler DeathEvent;

        public virtual void ReactToAttack()
        {
            Console.WriteLine($"{GetType().Name} {Name} is {_reactionToAttack}!");
        }
    }
}
