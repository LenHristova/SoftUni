namespace StorageMaster.Entities.Vehicles
{
    public class Truck : Vehicle
    {
        private const int CAPACITY = 5;

        public Truck() : base(CAPACITY)
        {
        }
    }
}