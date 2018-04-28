using System;

using P03_BarraksWars.Contracts;

namespace P03_BarraksWars.Models.Units
{
    public class Unit : IUnit
    {
        private int _health;
        private int _attackDamage;

        protected Unit(int health, int attackDamage)
        {
            SetInitialHealth(health);
            AttackDamage = attackDamage;
        }

        public int AttackDamage
        {
            get
            {
                return _attackDamage;
            }

            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Attack damage should be positive.");
                }

                _attackDamage = value;
            }
        }

        public int Health
        {
            get
            {
                return _health;
            }

            set
            {
                if (value < 0)
                {
                    _health = 0;
                }
                else
                {
                    _health = value;
                }
            }
        }

        private void SetInitialHealth(int health)
        {
            if (health <= 0)
            {
                throw new ArgumentException("Initial health should be positive.");
            }

            Health = health;
        }
    }
}
