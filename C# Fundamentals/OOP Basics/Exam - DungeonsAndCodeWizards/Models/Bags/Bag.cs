using System;
using System.Collections.Generic;
using System.Linq;

using DungeonsAndCodeWizards.Contracts;

namespace DungeonsAndCodeWizards.Models.Bags
{
    public abstract class Bag : IBag
    {
        private readonly ICollection<IItem> items;

        protected Bag(int capacity)
        {
            Capacity = capacity;
            items = new List<IItem>();
        }

        public int Capacity { get; private set; }

        public IReadOnlyCollection<IItem> Items => (IReadOnlyCollection<IItem>) items; 

        public int Load => Items.Sum(i => i.Weight);

        public void AddItem(IItem item)
        {
            if (Load + item.Weight > Capacity)
            {
                throw new InvalidOperationException(ExceptionMessages.FULL_BAG);
            }

            items.Add(item);
        }

        private bool IsEmpty => Items.Count == 0;

        public IItem GetItem(string name)
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException(ExceptionMessages.EMPTY_BAG);
            }

            var item = Items.FirstOrDefault(i => i.Name == name);

            if (item == null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.ITEM_NOT_IN_BAG, name));
            }

            items.Remove(item);
            return item;
        }
    }
}
