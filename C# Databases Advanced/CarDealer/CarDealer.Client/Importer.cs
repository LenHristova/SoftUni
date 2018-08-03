namespace CarDealer.Client
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml.Serialization;
    using AutoMapper;
    using Models;
    using Data;
    using Dtos;

    public class Importer
    {
        private readonly CarDealerContext context;
        private readonly IMapper mapper;

        public Importer(CarDealerContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public void ImportSales()
        {
            var rnd = new Random();

            var discounts = new int[] {0, 5, 10, 15, 20, 30, 40, 50};

            var customers = this.context.Customers.ToArray();

            var sales = new List<Sale>();

            var carsIds = this.context.Cars
                .Select(c => c.Id)
                .OrderBy(i => rnd.Next())
                .Take(rnd.Next(150, 359))
                .ToArray();

            foreach (var carId in carsIds)
            {
                var customer = customers[rnd.Next(0, 30)];

                var sale = new Sale
                {
                    CarId = carId,
                    CustomerId = customer.Id,
                    Discount = discounts[rnd.Next(0, 8)]
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

        public void ImportCustomers()
        {
            var deserialized = Deserialize<CustomerDto[]>("customers", "customers");

            var customers = new List<Customer>();
            foreach (var dto in deserialized)
            {
                var customer = this.mapper.Map<Customer>(dto);
                customers.Add(customer);
            }

            this.context.Customers.AddRange(customers);
            this.context.SaveChanges();
        }

        public void ImportCarsParts()
        {
            var rnd = new Random();

            var cars = this.context.Cars;

            var partsIds = Enumerable.Range(1, 131).ToArray();

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

        public void ImportCars()
        {
            var deserialized = Deserialize<CarDto[]>("cars", "cars");

            var cars = new List<Car>();
            foreach (var dto in deserialized)
            {
                var car = this.mapper.Map<Car>(dto);
                cars.Add(car);
            }

            this.context.Cars.AddRange(cars);
            this.context.SaveChanges();
        }

        public void ImportParts()
        {
            var deserialized = Deserialize<PartDto[]>("parts", "parts");

            var rnd = new Random();
            var parts = new List<Part>();
            foreach (var dto in deserialized)
            {
                var part = this.mapper.Map<Part>(dto);
                part.SupplierId = rnd.Next(1, 32);
                parts.Add(part);
            }

            this.context.Parts.AddRange(parts);
            this.context.SaveChanges();
        }

        public void ImportSuppliers()
        {
            var deserialized = Deserialize<SupplierDto[]>("suppliers", "suppliers");

            var suppliers = new List<Supplier>();
            foreach (var dto in deserialized)
            {
                var supplier = this.mapper.Map<Supplier>(dto);
                suppliers.Add(supplier);
            }
  
           this.context.Suppliers.AddRange(suppliers);
           this.context.SaveChanges();
        }

        private static TDesrialized Deserialize<TDesrialized>(string fileName, string root)
        {
            var path = $"../../../Xml/{fileName}.xml";
            var xmlString = File.ReadAllText(path);

            var serializer = new XmlSerializer(typeof(TDesrialized), new XmlRootAttribute(root));
            return (TDesrialized)serializer.Deserialize(new StringReader(xmlString));
        }
    }
}


