namespace CarDealer.Services.Implementations
{
    using Contracts;
    using Data;
    using Data.Models;
    using Models.Customers;
    using Models.Enums;
    using Models.Sales;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CustomerService : ICustomerService
    {
        private readonly CarDealerDbContext db;

        public CustomerService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<CustomerBaseModel> All()
            => this.db.Customers
                .OrderBy(c => c.Name)
                .Select(c => new CustomerBaseModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToList();

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

        public CustomerBoughtCarsModel SalesInfoById(int? id)
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

        public CustomerModel ById(int? id)
            => this.db.Customers
                .Where(c => c.Id == id)
                .Select(c => new CustomerModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    BirthDate = c.BirthDate,
                    IsYoungDriver = c.IsYoungDriver
                })
                .FirstOrDefault();

        public void Create(string name, DateTime birthDate, bool isYoungDriver)
        {
            var customer = new Customer
            {
                Name = name,
                BirthDate = birthDate,
                IsYoungDriver = isYoungDriver
            };

            this.db.Customers.Add(customer);
            this.db.SaveChanges();
        }

        public void Edit(int id, string name, DateTime birthDate, bool isYoungDriver)
        {
            var customer = this.db.Customers.Find(id);

            if (customer == null)
            {
                return;
            }

            customer.Name = name;
            customer.BirthDate = birthDate;
            customer.IsYoungDriver = isYoungDriver;

            this.db.SaveChanges();
        }

        public bool Exists(int? id)
            => this.db.Customers.Any(c => c.Id == id);

        public CustomerSaleInfoModel GetCustomerSaleInfoModel(int? id)
            => this.db.Customers
                .Where(c => c.Id == id)
                .Select(c => new CustomerSaleInfoModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    IsYoungDriver = c.IsYoungDriver
                })
                .FirstOrDefault();
    }
}
