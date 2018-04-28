using System.Collections.Generic;

using StorageMaster.Entities.Vehicles;

namespace StorageMaster.Entities.Storages
{
    public class Warehouse : Storage
    {
        private const int CAPACITY = 10;
        private const int GARAGE_SLOTS = 10;
        private static readonly IEnumerable<Vehicle> defaultVehicles = new List<Vehicle>
        {
            new Semi(),
            new Semi(),
            new Semi(),
        };

        public Warehouse(string name)
            : base(name, CAPACITY, GARAGE_SLOTS,defaultVehicles)
        {
        }
    }
}