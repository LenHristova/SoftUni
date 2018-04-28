using System.Collections.Generic;

using StorageMaster.Entities.Vehicles;

namespace StorageMaster.Entities.Storages
{
    public class DistributionCenter : Storage
    {
        private const int CAPACITY = 2;
        private const int GARAGE_SLOTS = 5;
        private static readonly IEnumerable<Vehicle> defaultVehicles = new List<Vehicle>
        {
            new Van(),
            new Van(),
            new Van(),
        };

        public DistributionCenter(string name)
            : base(name, CAPACITY, GARAGE_SLOTS, defaultVehicles)
        {
        }
    }
}