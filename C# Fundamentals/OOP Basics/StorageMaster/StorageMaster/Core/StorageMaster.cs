using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using StorageMaster.Entities.Products;
using StorageMaster.Entities.Storages;
using StorageMaster.Entities.Vehicles;
using StorageMaster.Factories;

namespace StorageMaster.Core
{
    public class StorageMaster
    {
        private IList<Product> products;
        private IList<Storage> storages;

        private ProductFactory productFactory;
        private StorageFactory storageFactory;

        private Vehicle currentVehicle;

        public StorageMaster()
        {
            this.products = new List<Product>();
            this.storages = new List<Storage>();
            this.productFactory = new ProductFactory();
            this.storageFactory = new StorageFactory();
        }

        public string AddProduct(string type, double price)
        {
            var product = productFactory.CreateProduct(type, price);

            products.Add(product);
            return $"Added {type} to pool";
        }

        public string RegisterStorage(string type, string name)
        {
            var storage = storageFactory.CreateStorage(type, name);

            storages.Add(storage);
            return $"Registered {name}";
        }

        public string SelectVehicle(string storageName, int garageSlot)
        {
            var storage = storages.FirstOrDefault(s => s.Name == storageName);

            if (storage == null)
            {
                //TODO check
            }

            currentVehicle = storage.GetVehicle(garageSlot);

            return $"Selected {currentVehicle.GetType().Name}";
        }

        public string LoadVehicle(IEnumerable<string> productNames)
        {
            var productNamesList = productNames.ToList();

            var count = 0;
            foreach (var productName in productNamesList)
            {
                if (currentVehicle.IsFull)
                {
                    break;
                }

                var product = products.LastOrDefault(p => p.GetType().Name == productName);
                if (product == null)
                {
                    throw new InvalidOperationException($"{productName} is out of stock!");
                }

                currentVehicle.LoadProduct(product);

                //TODO isLAstRemoved
                products.Remove(product);
                count++;
            }

            return $"Loaded { count}/{ productNamesList.Count} products into { currentVehicle.GetType().Name}";
        }

        public string SendVehicleTo(string sourceName, int sourceGarageSlot, string destinationName)
        {
            var sourceStorage = storages.FirstOrDefault(s => s.Name == sourceName);

            if (sourceStorage == null)
            {
                throw new InvalidOperationException("Invalid source storage!");
            }

            var destinationStorage = storages.FirstOrDefault(s => s.Name == destinationName);

            if (destinationStorage == null)
            {
                throw new InvalidOperationException("Invalid destination storage!");
            }

            var vehicle = sourceStorage.GetVehicle(sourceGarageSlot);

            var destinationGarageSlot = sourceStorage.SendVehicleTo(sourceGarageSlot, destinationStorage);

            return $"Sent {vehicle.GetType().Name} to {destinationName} (slot {destinationGarageSlot})";
        }

        public string UnloadVehicle(string storageName, int garageSlot)
        {
            var storage = storages.FirstOrDefault(s => s.Name == storageName);

            if (storage == null)
            {
                //TODO
            }

            var vehicle = storage?.GetVehicle(garageSlot);
            var productsInVehicle = vehicle?.Trunk.Count;

            var unloadedProductsCount = storage?.UnloadVehicle(garageSlot);

            return $"Unloaded { unloadedProductsCount}/{productsInVehicle } products at { storageName}";
        }

        public string GetStorageStatus(string storageName)
        {
            var storage = storages.FirstOrDefault(s => s.Name == storageName);

            if (storage == null)
            {
                //TODO
            }

            var orderedProducts = storage.Products
                .GroupBy(p => p.GetType().Name)
                .ToDictionary(p => p.Key, p => p.ToList())
                .OrderByDescending(p => p.Value.Count)
               .ThenBy(p => p.Key);

            var result = orderedProducts.Select(p => $"{p.Key} ({p.Value.Count})");

            var stock = $"Stock ({storage.Products.Sum(p => p.Weight)}/{storage.Capacity}): [{string.Join(", ", result)}]";

            var garage = storage.Garage.Select(v => v == null ? "empty" : $"{v.GetType().Name}");

            var m = $"Garage: [{string.Join("|", garage)}]";
             return stock + Environment.NewLine + m;
        }

        public string GetSummary()
        {
            var ordered = storages
                .OrderByDescending(s => s.Products.Sum(p => p.Price));

            var sb = new StringBuilder();
            foreach (var storage in ordered)
            {
                var sum = storage.Products.Sum(p => p.Price);

                sb.AppendLine($"{storage.Name}:")
                    .AppendLine($"Storage worth: ${sum:F2}");
            }

            return sb.ToString().Trim();
        }

    }
}