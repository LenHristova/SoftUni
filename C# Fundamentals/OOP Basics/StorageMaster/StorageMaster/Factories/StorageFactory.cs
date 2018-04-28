using System;

using StorageMaster.Entities.Storages;

namespace StorageMaster.Factories
{
    public class StorageFactory
    {
        public Storage CreateStorage(string type, string name)
        {
            switch (type)
            {
                case nameof(AutomatedWarehouse):
                    return new AutomatedWarehouse(name);
                case nameof(DistributionCenter):
                    return new DistributionCenter(name);
                case nameof(Warehouse):
                    return new Warehouse(name);
                default:
                    throw new InvalidOperationException("Invalid storage type!");
            }
        }
    }
}