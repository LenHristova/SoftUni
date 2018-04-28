using System;

using DungeonsAndCodeWizards.Contracts;

namespace DungeonsAndCodeWizards.Models.Characters
{
    public abstract class Character : ICharacter
    {
        private string name;
        private double health;
        private double armor;

        protected Character(string name, double health, double armor, double abilityPoints, IBag bag, Faction faction)
        {
            Name = name;
            BaseHealth = health;
            Health = health;
            BaseArmor = armor;
            Armor = armor;
            AbilityPoints = abilityPoints;
            Bag = bag;
            Faction = faction;
            IsAlive = true;
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.EMPTY_NAME);
                }

                name = value;
            }
        }

        public double BaseHealth { get; }

        public double Health
        {
            get => health;
            private set
            {

                if (value <= 0)
                {
                    IsAlive = false;
                    health = 0;
                }
                else
                {
                    health = Math.Min(value, BaseHealth);
                }
            }
        }

        public double BaseArmor { get; }

        public double Armor
        {
            get => armor;
            private set
            {
                armor = value <= 0 ? 0 : Math.Min(value, BaseArmor);
            }
        }

        public double AbilityPoints { get; }

        public IBag Bag { get; }

        public Faction Faction { get; }

        public bool IsAlive { get; private set; }

        public virtual double RestHealMultiplier => 0.2;

        public void TakeDamage(double hitPoints)
        {
            EnsureAlive();

            var healthDamage = hitPoints - Armor;
            Armor -= hitPoints;

            if (healthDamage > 0)
            {
                Health -= healthDamage;
            }
        }

        public void Rest()
        {
            EnsureAlive();
            Health += (BaseHealth * RestHealMultiplier);
        }

        public void UseItem(IItem item)
        {
            EnsureAlive();
            item.AffectCharacter(this);
        }

        public void UseItemOn(IItem item, ICharacter character)
        {
            if (IsAlive)
            {
                character.UseItem(item);
            }
        }

        public void GiveCharacterItem(IItem item, ICharacter character)
        {
            if (IsAlive)
            {
                character.ReceiveItem(item);
            }
        }

        public void ReceiveItem(IItem item)
        {
            if (IsAlive)
            {
                Bag.AddItem(item);
            }
        }

        public void BeHealed(double healPoints)
        {
            Health += healPoints;
        }

        public void RestoreArmor()
        {
            Armor = BaseArmor;
        }

        private string Status => IsAlive ? "Alive" : "Dead";

        public override string ToString()
        {
            return $"{Name} - HP: {Health}/{BaseHealth}, AP: {Armor}/{BaseArmor}, Status: {Status}";
        }

        public void BePoisoned(double poisonPoints)
        {
            Health -= poisonPoints;
        }

        public void EnsureAlive()
        {
            if (!IsAlive)
            {
                throw new InvalidOperationException(ExceptionMessages.ALIVE_FOR_ACTION);
            }
        }
    }
}
