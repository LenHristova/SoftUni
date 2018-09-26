namespace CarDealer.Services.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Data;
    using Models.Customers;
    using Models.Enums;
    using Models.Sales;

    public class CustomerService : ICustomerService
    {
        private readonly CarDealerDbContext db;

        public CustomerService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<CustomerModel> OrderedByBirthDate(OrderType order)
        {
            var customersQuery = this.db.Customers.AsQueryable();

            switch (order)
            {
                case OrderType.Ascending:
                    customersQuery = customersQuery
                        .OrderBy(c => c.BirthDate)
                        .ThenBy(c => c.Name);
                    break;
                case OrderType.Descending:
                    customersQuery = customersQuery
                        .OrderByDescending(c => c.BirthDate)
                        .ThenBy(c => c.Name);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(order), order, null);
            }

            return customersQuery
                .Select(c => new CustomerModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    BirthDate = c.BirthDate,
                    IsYoungDriver = c.IsYoungDriver
                })
                .ToList();
        }

        public CustomerBoughtCarsModel ById(int? id)
            => this.db.Customers
                .Where(c => c.Id == id)
                .Select(c => new CustomerBoughtCarsModel
                {
                    Name = c.Name,
                    BoughtCars = c.Sales.Count,
                    SaleInfo = c.Sales
                        .Select(s => new SaleBaseModel
                            {
                                CarPrice = s.Car.Parts
                                      .Sum(p => p.Part.Price),
                                Discount = s.Discount + (c.IsYoungDriver ? 0.05 : 0)
                            })
                        .ToList()
                })
                .FirstOrDefault();
    }
}
