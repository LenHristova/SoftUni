using System;

using StorageMaster.Entities.Vehicles;

namespace StorageMaster.Factories
{
    public class VehicleFactory
    {
        public Vehicle CreateVehicle(string type)
        {
            switch (type)
            {
                case nameof(Van):
                    return new Van();
                case nameof(Truck):
                    return new Truck();
                case nameof(Semi):
                    return new Semi();
                default:
                    throw new InvalidOperationException("Invalid vehicle type!");
            }
        }
    }
}