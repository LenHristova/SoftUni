namespace CarDealer.Client
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Linq;
    using Models;
    using Data;
    using Newtonsoft.Json;

    public class Importer
    {
        private readonly CarDealerContext context;

        public Importer(CarDealerContext context)
        {
            this.context = context;
        }

        public void ImportCustomers()
        {
            var jsonString = File.ReadAllText("Json/customers.json");
            var deserialized = JsonConvert.DeserializeObject<Customer[]>(jsonString);

            var customers = deserialized
                .Where(IsValid)
                .ToArray();

            this.context.Customers.AddRange(customers);
            this.context.SaveChanges();

            var rnd = new Random();
            var discounts = new int[] { 0, 5, 10, 15, 20, 30, 40, 50 };

            var carsCount = this.context.Cars.Count();
            var carsIds = this.context.Cars
                .Select(c => c.Id)
                .OrderBy(i => rnd.Next())
                .Take(rnd.Next(carsCount / 3, carsCount))
                .ToArray();

            var sales = new List<Sale>();
            foreach (var carId in carsIds)
            {
                var customer = customers[rnd.Next(0, customers.Length)];

                var sale = new Sale
                {
                    CarId = carId,
                    CustomerId = customer.Id,
                    Discount = discounts[rnd.Next(0, discounts.Length)]
                };

                if (customer.IsYoungDriver)
                {
                    sale.Discount += 5;
                }

                sales.Add(sale);
            }

            this.context.Sales.AddRange(sales);
            this.context.SaveChanges();
        }

        public void ImportCars()
        {
            var jsonString = File.ReadAllText("Json/cars.json");
            var deserialized = JsonConvert.DeserializeObject<Car[]>(jsonString);

            var cars = deserialized
                .Where(IsValid)
                .ToArray();

            this.context.Cars.AddRange(cars);
            this.context.SaveChanges();

            var rnd = new Random();

            var partsCount = this.context.Parts.Count();
            var partsIds = Enumerable.Range(1, partsCount).ToArray();

            var carsParts = new List<CarPart>();
            foreach (var car in cars)
            {
                var carPartsIds = partsIds
                    .OrderBy(i => rnd.Next())
                    .Take(rnd.Next(10, 21))
                    .ToArray();

                foreach (var partId in carPartsIds)
                {
                    var carPart = new CarPart
                    {
                        CarId = car.Id,
                        PartId = partId
                    };

                    carsParts.Add(carPart);
                }
            }

            context.CarsParts.AddRange(carsParts);
            context.SaveChanges();
        }

        public void ImportParts()
        {
            var jsonString = File.ReadAllText("Json/parts.json");
            var deserialized = JsonConvert.DeserializeObject<Part[]>(jsonString);

            var rnd = new Random();
            var parts = new List<Part>();
            var suppliersCount = this.context.Suppliers.Count();

            foreach (var part in deserialized)
            {
                if (IsValid(part))
                {
                    part.SupplierId = rnd.Next(1, suppliersCount);
                    parts.Add(part);
                }
            }

            this.context.Parts.AddRange(parts);
            this.context.SaveChanges();
        }

        public void ImportSuppliers()
        {
            var jsonString = File.ReadAllText("Json/suppliers.json");
            var deserialized = JsonConvert.DeserializeObject<Supplier[]>(jsonString);

            var suppliers = deserialized
                .Where(IsValid)
                .ToArray();

            this.context.Suppliers.AddRange(suppliers);
            this.context.SaveChanges();
        }

        private static bool IsValid(object obj)
            => Validator.TryValidateObject(
                obj,
                new System.ComponentModel.DataAnnotations.ValidationContext(obj),
                new List<ValidationResult>(),
                true);
    }
}


