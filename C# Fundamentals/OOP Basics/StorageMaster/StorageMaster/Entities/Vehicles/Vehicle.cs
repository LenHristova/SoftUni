using System;
using System.Collections.Generic;
using System.Linq;

using StorageMaster.Entities.Products;

namespace StorageMaster.Entities.Vehicles
{
    public abstract class Vehicle
    {
        private IList<Product> truck;

        protected Vehicle(int capacity)
        {
            Capacity = capacity;
            truck = new List<Product>();
        }

        public int Capacity { get; }

        public IReadOnlyCollection<Product> Trunk => (IReadOnlyCollection<Product>) truck;

        public bool IsFull => truck.Sum(p => p.Weight) >= Capacity;

        public bool IsEmpty => !truck.Any();

        public void LoadProduct(Product product)
        {
            if (IsFull)
            {
                throw new InvalidOperationException("Vehicle is full!");
            }

            truck.Add(product);
        }

        public Product Unload()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("No products left in vehicle!");
            }

            var product = truck.Last();
            truck.Remove(product);

            return product;
        }
    }
}