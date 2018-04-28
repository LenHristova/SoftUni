namespace StorageMaster.Entities.Products
{
    public class HardDrive : Product
    {
        private const double WEIGHT = 1;

        public HardDrive(double price) : base(price, WEIGHT)
        {
        }
    }
}