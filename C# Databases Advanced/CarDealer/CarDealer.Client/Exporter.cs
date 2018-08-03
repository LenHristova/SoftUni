namespace CarDealer.Client
{
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using Data;
    using Dtos;

    public class Exporter
    {
        private readonly CarDealerContext context;

        public Exporter(CarDealerContext context)
        {
            this.context = context;
        }

        public void ExportSalesWithDiscounts()
        {
            var sales = this.context.Sales
                .Select(s => new SaleDto()
                {
                    CustomerName = s.Customer.Name,
                    Car = new CarInfoDto
                    {
                        Make = s.Car.Make,
                        Model = s.Car.Model,
                        TravelledDistance = s.Car.TravelledDistance
                    },
                    Discount = s.Discount / 100.0,
                    Price = s.Car.Parts.Sum(p => p.Part.Price),
                    PriceWithDiscount = s.Car.Parts.Sum(p => p.Part.Price) * (1 - (s.Discount / 100.0M))
                })
                .ToArray();

            Serialize(sales, "sales-discounts", "sales");
        }

        //public void ExportSalesByCustomer()
        //{
        //    var sales = this.context.Customers
        //        .Select(c => new CustomerAllPurchasesDto()
        //        {
        //            Name = c.Name,
        //            AllSpendMoney = c.Sales.Select(s => s.Car.Parts.Select(p => p.Part.Price).Sum() * (1 - (s.Discount / 100.0M))).ToArray(),
        //            BoughtCars = c.Sales.Count
        //        })
        //        .ToArray();


        //    var customerSales = sales
        //        .OrderByDescending(c => c.AllSpendMoney.Sum())
        //        .ThenByDescending(c => c.BoughtCars)
        //        .Select(c => new CustomerAllPurchasesInfoDto()
        //        {
        //            Name = c.Name,
        //            SpendMoney = c.AllSpendMoney.Sum().ToString("F"),
        //            BoughtCars = c.BoughtCars
        //        })
        //        .ToArray();

        //    Serialize(customerSales, "customers-total-sales", "customers");
        //}

        public void ExportSalesByCustomer()
        {
            var sales = this.context.Sales
                .Select(s => new SaleDto()
                {
                    CustomerName = s.Customer.Name,
                    PriceWithDiscount = s.Car.Parts.Sum(p => p.Part.Price) * (1 - (s.Discount / 100.0M))
                })
                .ToArray();

            var customerSales = sales
                .GroupBy(c => c.CustomerName)
                .Select(c => new SalesByCustomerDto
                {
                    Name = c.Key,
                    AllSpendMoney = c.ToList().Select(n => n.PriceWithDiscount).Sum(),
                    BoughtCars = c.Count()

                })
                .OrderByDescending(c => c.AllSpendMoney)
                .ThenByDescending(c => c.BoughtCars)
                .Select(c => new CustomerPurchasesInfoDto()
                {
                    Name = c.Name,
                    SpendMoney = c.AllSpendMoney.ToString("F"),
                    BoughtCars = c.BoughtCars
                })
                .ToArray();

            Serialize(customerSales, "customers-total-sales", "customers");
        }

        public void ExportCarsWithParts()
        {
            var carsParts = this.context.CarsParts
                .Select(c => new CarPartDto()
                {
                    Car = new CarInfoDto()
                    {
                        Id = c.Car.Id,
                        Make = c.Car.Make,
                        Model = c.Car.Model,
                        TravelledDistance = c.Car.TravelledDistance
                    },
                    Part = new PartInfoDto()
                    {
                        Name = c.Part.Name,
                        Price = c.Part.Price
                    }   
                })
                .ToArray();

            var cars = carsParts
                .GroupBy(cp => cp.Car.Id)
                .Select(cp => new CarWithPartsDto()
                {
                    Make = cp.Select(x => x.Car.Make).First(),
                    Model = cp.Select(x => x.Car.Model).First(),
                    TravelledDistance = cp.Select(x => x.Car.TravelledDistance).First(),
                    Parts = cp
                        .Select(p => new PartInfoDto()
                        {
                            Name = p.Part.Name,
                            Price = p.Part.Price
                        })
                        .ToArray()

                })
                .ToArray();

            //var cars = this.context.Cars
            //    .Select(c => new CarWithPartsDto()
            //    {
            //        Make = c.Make,
            //        Model = c.Model,
            //        TravelledDistance = c.TravelledDistance,
            //        Parts = c.Parts
            //            .Select(p => new CarPartDto
            //            {
            //                Name = p.Part.Name,
            //                Price = p.Part.Price
            //            })
            //    })
            //    .ToArray();

            Serialize(cars, "cars-and-parts", "cars");
        }

        public void ExportLocalSuppliers()
        {
            var localSuppliers = this.context.Suppliers
                .Where(s => s.IsImporter == false)
                .Select(s => new LocalSupplierDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    PartsCount = s.Parts.Count
                })
                .ToArray();

            Serialize(localSuppliers, "local-supliers", "suppliers");
        }

        public void ExportCarsByМake(string make)
        {
            var cars = this.context.Cars
                .Where(c => c.Make == make)
                .OrderBy(c => c.Model)
                .ThenByDescending(c => c.TravelledDistance)
                .Select(c => new CarModelDto()
                {
                    Id = c.Id,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance
                })
                .ToArray();

            Serialize(cars, "cars-by-make", "cars");
        }

        public void ExportCarsByDistance(long minDistance)
        {
            var cars = this.context.Cars
                .Where(c => c.TravelledDistance > minDistance)
                .OrderBy(c => c.Make)
                .ThenBy(c => c.Model)
                .Select(c => new CarDto
                {
                    Make = c.Make,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance
                })
                .ToArray();

            Serialize(cars, "cars-by-distance", "cars");
        }

        private static void Serialize<TDto>(TDto[] dtoArray, string fileName, string root)
        {
            var path = $"../../../Xml/{fileName}.xml";
            var sb = new StringBuilder();
            var namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            var serializer = new XmlSerializer(typeof(TDto[]), new XmlRootAttribute(root));
            serializer.Serialize(new StringWriter(sb), dtoArray, namespaces);
            File.WriteAllText(path, sb.ToString());
        }
    }
}
