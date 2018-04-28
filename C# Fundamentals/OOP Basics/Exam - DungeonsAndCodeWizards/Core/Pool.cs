using System;
using System.Collections.Generic;
using System.Linq;
using DungeonsAndCodeWizards.Contracts;

namespace DungeonsAndCodeWizards.Core
{
    public class Pool : IPool
    {
        private readonly Stack<IItem> itemPool;

        public Pool(Stack<IItem> itemPool)
        {
            this.itemPool = itemPool;
        }

        public string AddItemToPool(IItem item)
        {
            itemPool.Push(item);
            return $"{item.Name} added to pool.";
        }

        public IItem PickUpItem()
        {
            if (!itemPool.Any())
            {
                throw new InvalidOperationException(ExceptionMessages.EMPTY_POOL);
            }

            return itemPool.Pop();
        }
    }
}