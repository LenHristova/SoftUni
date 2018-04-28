using System;

using DungeonsAndCodeWizards.Contracts;
using DungeonsAndCodeWizards.Models.Bags;

namespace DungeonsAndCodeWizards.Models.Characters
{
    public class Cleric : Character, IHealable
    {
        private const double BASE_HEALTH = 50;
        private const double BASE_ARMOR = 25;
        private const double ABILITY_POINTS = 40;

        public Cleric(string name, Faction faction)
            : base(name, BASE_HEALTH, BASE_ARMOR, ABILITY_POINTS, new Backpack(), faction)
        {
        }

        public override double RestHealMultiplier => 0.5;

        public void Heal(ICharacter character)
        {
            EnsureAlive();
            character.EnsureAlive();

            if (Faction != character.Faction)
            {
                throw new InvalidOperationException("Cannot heal enemy character!");
            }

            character.BeHealed(AbilityPoints);
        }
    }
}
