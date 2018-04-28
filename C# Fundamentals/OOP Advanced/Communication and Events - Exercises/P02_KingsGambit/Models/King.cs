using System;
using System.Collections.Generic;

using P02_KingsGambit.Contracts;

namespace P02_KingsGambit.Models
{
    public class King : IKing
    {
        private IList<ISubordinate> _subordinates;

        public King(string name, IEnumerable<ISubordinate> subordinates)
        {
            Name = name;
            InitializeSubordinates(subordinates);
        }

        private void InitializeSubordinates(IEnumerable<ISubordinate> subordinates)
        {
            _subordinates = new List<ISubordinate>();
            foreach (var subordinate in subordinates)
            {
                AddSubordinate(subordinate);
            }
        }

        public string Name { get; }

        public IReadOnlyCollection<ISubordinate> Subordinates => (IReadOnlyCollection<ISubordinate>)_subordinates; 

        public event BeAttackedEventHandler GetAttackedEvent;

        public void BeAttacked()
        {
            Console.WriteLine($"{GetType().Name} {Name} is under attack!");

            GetAttackedEvent?.Invoke();
        }

        public void AddSubordinate(ISubordinate subordinate)
        {
            _subordinates.Add(subordinate);
            GetAttackedEvent += subordinate.ReactToAttack;
        }
    }
}
