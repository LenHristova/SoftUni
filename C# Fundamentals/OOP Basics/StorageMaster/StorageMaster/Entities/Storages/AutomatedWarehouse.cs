using System.Collections.Generic;

using StorageMaster.Entities.Vehicles;

namespace StorageMaster.Entities.Storages
{
    public class AutomatedWarehouse : Storage
    {
        private const int CAPACITY = 1;
        private const int GARAGE_SLOTS = 2;
        private static readonly IEnumerable<Vehicle> defaultVehicles = new List<Vehicle>
        {
            new Truck()
        };

        public AutomatedWarehouse(string name) 
            : base(name, CAPACITY, GARAGE_SLOTS, defaultVehicles)
        {
        }
    }
}