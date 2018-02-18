using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace P05_GreedyTimes
{
    public class Bag
    {
        public List<Item> Items { get; set; }
        public long MaxCapacity { get; set; }
        public long FreeCapacity => MaxCapacity - Items.Select(x => x.Amount).Sum();

        public Bag(long maxCapacity)
        {
            Items = new List<Item>();
            MaxCapacity = maxCapacity;
        }

        public void TryToAddItemAmount(string itemName, long itemAmount)
        {
            if (FreeCapacity < itemAmount) return;

            if (itemName.Length == 3)
            {
                if (ItemsAmount("Gem") >= ItemsAmount("Cash") + itemAmount)
                {
                    AddItem(itemName, itemAmount, "Cash");
                }
            }
            else if (itemName.ToLower().EndsWith("gem"))
            {
                if (ItemsAmount("Gold") >= ItemsAmount("Gem") + itemAmount)
                {
                    AddItem(itemName, itemAmount, "Gem");
                }
            }
            else if (itemName.ToLower() == "gold")
            {
                AddItem(itemName, itemAmount, "Gold");
            }
        }

        private long ItemsAmount(string itemType)
        {
            return Items.Select(x => x.Type == itemType ? x.Amount : 0).Sum();
        }

        private void AddItem(string itemName, long amount, string type)
        {
            var currentItem = Items.FirstOrDefault(x => x.Name == itemName);
            if (currentItem == null)
            {
                currentItem = new Item(itemName, type);
                Items.Add(currentItem);
            }

            currentItem.Amount += amount;
        }

        public override string ToString()
        {
            var itemsGroupedByType = Items.GroupBy(i => i.Type)
                .ToDictionary(x => x.Key, x => x.ToList());

            var sb = new StringBuilder();
            foreach (var item in itemsGroupedByType)
            {
                var orderedItems = item.Value
                    .OrderByDescending(x => x.Name)
                    .ThenBy(x => x.Amount);

                sb.AppendLine($"<{item.Key}> ${item.Value.Sum(x => x.Amount)}");
                sb.AppendJoin(Environment.NewLine, orderedItems);
                sb.AppendLine();
            }

            return sb.ToString().TrimEnd();
        }
    }
}