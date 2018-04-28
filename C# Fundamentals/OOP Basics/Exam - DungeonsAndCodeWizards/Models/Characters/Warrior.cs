using System;

using DungeonsAndCodeWizards.Contracts;
using DungeonsAndCodeWizards.Models.Bags;

namespace DungeonsAndCodeWizards.Models.Characters
{
    public class Warrior : Character, IAttackable
    {
        private const double BASE_HEALTH = 100;
        private const double BASE_ARMOR = 50;
        private const double ABILITY_POINTS = 40;

        public Warrior(string name, Faction faction)
            : base(name, BASE_HEALTH, BASE_ARMOR, ABILITY_POINTS, new Satchel(), faction)
        {
        }

        public void Attack(ICharacter character)
        {
            EnsureAlive();
            character.EnsureAlive();

            if (this == character)
            {
                throw new InvalidOperationException("Cannot attack self!");
            }

            if (Faction == character.Faction)
            {
                var message = $"Friendly fire! Both characters are from {Faction} faction!";
                throw new ArgumentException(message);
            }

            character.TakeDamage(AbilityPoints);
        }
    }
}
