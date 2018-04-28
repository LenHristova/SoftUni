using System;

using StorageMaster.Entities.Products;

namespace StorageMaster.Factories
{
    public class ProductFactory
    {
        public Product CreateProduct(string type, double price)
        {
            switch (type)
            {
                case nameof(Gpu):
                    return new Gpu(price);
                case nameof(HardDrive):
                    return new HardDrive(price);
                case nameof(Ram):
                    return new Ram(price);
                case nameof(SolidStateDrive):
                    return new SolidStateDrive(price);
                    default:
                        throw new InvalidOperationException("Invalid product type!");
            }
        }
    }
}