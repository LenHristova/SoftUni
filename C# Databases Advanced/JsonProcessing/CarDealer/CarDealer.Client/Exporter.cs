namespace CarDealer.Client
{
    using System;
    using System.IO;
    using System.Linq;
    using Data;
    using Newtonsoft.Json;

    public class Exporter
    {
        private readonly CarDealerContext context;

        public Exporter(CarDealerContext context)
        {
            this.context = context;
        }

        public void ExportSalesWithAppliedDiscount()
        {
            var sales = this.context.Sales
                .Select(s => new
                {
                    car = new
                    {
                        Make = s.Car.Make,
                        Model = s.Car.Model,
                        TravelledDistance = s.Car.TravelledDistance
                    },
                    customerName = s.Customer.Name,
                    Discount = s.Discount / 100.0,
                    price = s.Car.Parts.Sum(p => p.Part.Price),
                    priceWithDiscount = s.Car.Parts.Sum(p => p.Part.Price) * (1 - (s.Discount / 100.0M))
                })
                .ToArray();

            var serialized = JsonConvert.SerializeObject(sales, new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            });
            File.WriteAllText("Json/sales-discounts.json", serialized);
        }


        public void ExportTotalSalesByCustomer()
        {
            var cars = this.context.Customers
                .Where(c => c.Sales.Any())
                .Select(c => new
                {
                    fullName = c.Name,
                    boughtCars = c.Sales.Count,
                    spentMoney = c.Sales.Sum(s => s.Car.Parts.Sum(p => p.Part.Price)),
                })
                .OrderByDescending(c => c.spentMoney)
                .ThenByDescending(c => c.boughtCars)
                .ToArray();

            var serialized = JsonConvert.SerializeObject(cars, new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            });
            File.WriteAllText("Json/customers-total-sales.json", serialized);
        }

        public void ExportCarsAndParts()
        {
            var cars = this.context.Cars
                .Select(c => new
                {
                    car = new
                    {
                        Make = c.Make,
                        Model = c.Model,
                        TravelledDistance = c.TravelledDistance
                    },
                    parts = c.Parts
                        .Select(p => new
                        {
                            Name = p.Part.Name,
                            Price = p.Part.Price
                        })
                        .ToArray()
                })
                .ToArray();

            var serialized = JsonConvert.SerializeObject(cars, new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            });
            File.WriteAllText("Json/cars-and-parts.json", serialized);
        }

        public void ExportLocalSuppliers()
        {
            var suppliers = this.context.Suppliers
                .Where(s => s.IsImporter == false)
                .Select(s => new
                {
                    Id = s.Id,
                    Name = s.Name,
                    PartsCount = s.Parts.Count
                })
                .ToArray();

            var serialized = JsonConvert.SerializeObject(suppliers, new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            });
            File.WriteAllText("Json/local-suppliers.json", serialized);
        }

        public void ExportCarsByMake(string make)
        {
            var cars = this.context.Cars
                .Where(c => string.Equals(c.Make, make, StringComparison.CurrentCultureIgnoreCase))
                .OrderBy(c => c.Model)
                .ThenByDescending(c => c.TravelledDistance)
                .Select(c => new
                {
                    Id = c.Id,
                    Make = c.Make,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance
                })
                .ToArray();

            var serialized = JsonConvert.SerializeObject(cars, new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            });
            File.WriteAllText($"Json/{make.ToLower()}-cars.json", serialized);
        }

        public void ExportOrderedCustomers()
        {
            var customers = this.context.Customers
                .OrderBy(c => c.BirthDate)
                .ThenBy(c => c.IsYoungDriver)
                .ToList();

            var serialized = JsonConvert.SerializeObject(customers, new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            });
            File.WriteAllText("Json/ordered-customers.json", serialized);
        }
    }
}
