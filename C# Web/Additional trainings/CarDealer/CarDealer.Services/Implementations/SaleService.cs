namespace CarDealer.Services.Implementations
{
    using Contracts;
    using Data;
    using Data.Models;
    using Models.Cars;
    using Models.Customers;
    using Models.Enums;
    using Models.Parts;
    using Models.Sales;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public class SaleService : ISaleService
    {
        private readonly CarDealerDbContext db;

        public SaleService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<SaleModel> All(SaleType type)
        {
            Expression<Func<Sale, bool>> func = s => true;
            switch (type)
            {
                case SaleType.All:
                    break;
                case SaleType.Discounted:
                    func = s => s.Discount > 0;
                    break;
                case SaleType.Clear:
                    func = s => s.Discount <= 0;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            return this.db.Sales
                .Where(func)
                .Select(s => new SaleModel
                {
                    Id = s.Id,
                    Car = new CarBaseModel
                    {
                        Id = s.CarId,
                        Make = s.Car.Make,
                        Model = s.Car.Model
                    },
                    Customer = new CustomerBaseModel
                    {
                        Id = s.CustomerId,
                        Name = s.Customer.Name
                    },
                    CarPrice = s.Car.Parts.Sum(p => p.Part.Price),
                    Discount = s.Discount + (s.Customer.IsYoungDriver ? 0.05 : 0)
                })
                .ToList();
        }

        public SaleFullInfoModel ById(int? id)
            => this.db.Sales
                .Where(s => s.Id == id)
                .Select(s => new SaleFullInfoModel
                {
                    Car = new CarWithPartsModel()
                    {
                        Make = s.Car.Make,
                        Model = s.Car.Model,
                        TraveledDistance = s.Car.TraveledDistance,
                        Parts = s.Car.Parts
                            .Select(p => new PartBaseModel
                            {
                                Name = p.Part.Name,
                                Price = p.Part.Price
                            })
                            .ToList()
                    },
                    Customer = new CustomerModel
                    {
                        Id = s.CustomerId,
                        Name = s.Customer.Name,
                        BirthDate = s.Customer.BirthDate,
                        IsYoungDriver = s.Customer.IsYoungDriver
                    },
                    Discount = s.Discount,
                    CarPrice = s.Car.Parts.Sum(p => p.Part.Price)
                })
                .FirstOrDefault();

        public int Create(int customerId, int carId, double discount)
        {
            var sale = new Sale
            {
                CustomerId = customerId,
                CarId = carId,
                Discount = discount
            };

            this.db.Sales.Add(sale);
            this.db.SaveChanges();

            return sale.Id;
        }
    }
}
