using System;
using System.Collections.Generic;
using System.Linq;

using StorageMaster.Entities.Products;
using StorageMaster.Entities.Vehicles;

namespace StorageMaster.Entities.Storages
{
    public abstract class Storage
    {
        private Vehicle[] vehicles;
        private IList<Product> products;

        protected Storage(string name, int capacity, int garageSlots, IEnumerable<Vehicle> vehicles)
        {
            Name = name;
            Capacity = capacity;
            GarageSlots = garageSlots;

            //TODO Vehicle capacity?

            products = new List<Product>();
            this.vehicles = new Vehicle[garageSlots];

            AddVehicles(vehicles.ToList());
        }

        private void AddVehicles(IList<Vehicle> defaultVehicles)
        {
            for (int i = 0; i < defaultVehicles.Count; i++)
            {
                vehicles[i] = defaultVehicles[i];
            }
        }

        public string Name { get; }

        public int Capacity { get; }

        public int GarageSlots { get; }

        public bool IsFull => products.Sum(p => p.Weight) >= Capacity;

        public IReadOnlyCollection<Vehicle> Garage => (IReadOnlyCollection<Vehicle>)vehicles;

        public IReadOnlyCollection<Product> Products => (IReadOnlyCollection<Product>)products;

        public Vehicle GetVehicle(int garageSlot)
        {
            //TODO smaller? <0

            if (garageSlot >= GarageSlots)
            {
                throw new InvalidOperationException("Invalid garage slot!");
            }

            if (vehicles[garageSlot] == null)
            {
                throw new InvalidOperationException("No vehicle in this garage slot!");
            }

            //TODO Remove?
            return vehicles[garageSlot];
        }

        public int SendVehicleTo(int garageSlot, Storage deliveryLocation)
        {
            var vehicle = GetVehicle(garageSlot);

            var freeSlots = deliveryLocation.Garage.Any(v => v == null);

            if (!freeSlots)
            {
                throw new InvalidOperationException("No room in garage!");
            }

            //TODO null?
            vehicles[garageSlot] = null;

            int i;
            for ( i = 0; i < deliveryLocation.GarageSlots; i++)
            {
                if (deliveryLocation.vehicles[i] == null)
                {
                    deliveryLocation.vehicles[i] = vehicle;
                    break;
                }
            }

            return i;
        }

        public int UnloadVehicle(int garageSlot)
        {
            if (IsFull)
            {
                throw new InvalidOperationException("Storage is full!");
            }

            var vehicle = GetVehicle(garageSlot);

            int count = 0;
            while (!IsFull && !vehicle.IsEmpty)
            {
                products.Add(vehicle.Unload());
                count++;
            }

            return count;
        }
    }
}