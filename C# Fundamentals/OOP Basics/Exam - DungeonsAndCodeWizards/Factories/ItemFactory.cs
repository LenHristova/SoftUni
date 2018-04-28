using System;
using System.Linq;
using System.Reflection;

using DungeonsAndCodeWizards.Contracts;

namespace DungeonsAndCodeWizards.Factories
{
    public class ItemFactory : IItemFactory
    {
        public IItem CreateItem(string itemName)
        {
            var itemType = Assembly.GetCallingAssembly()
                .GetTypes()
                .FirstOrDefault(t => t.Name == itemName);

            if (itemType == null)
            {
                throw new ArgumentException($"Invalid item \"{itemName}\"!");
            }

            if (!typeof(IItem).IsAssignableFrom(itemType))
            {
                throw new InvalidOperationException($"{itemName} is not IItem");
            }

            var item = (IItem)Activator.CreateInstance(itemType);

            return item;
        }
    }
}
