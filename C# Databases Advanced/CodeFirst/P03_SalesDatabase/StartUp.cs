using System;
using P03_SalesDatabase.Data;
using P03_SalesDatabase.Data.Models;

namespace P03_SalesDatabase
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
           // Seed();
        }

        private static void Seed()
        {
            var products = new[]
                        {
                new Product
                {
                    Name = "Cheese", Quantity = 2.15, Price = 5.1M
                },
                new Product
                {
                    Name = "Bread", Quantity = 3, Price = 1.2M
                },
                new Product
                {
                    Name = "Salami", Quantity = 1, Price = 5.8M
                },
            };

            var customers = new[]
            {
                new Customer
                {
                    Name = "Pesho",
                    Email = "pesho@abv.bg",
                    CreditCardNumber = "12354989"
                },
                new Customer
                {
                    Name = "Gosho",
                    Email = "gosho@abv.bg",
                    CreditCardNumber = "12354989"
                },
                new Customer
                {
                    Name = "Peshka",
                    Email = "peshka@abv.bg",
                    CreditCardNumber = "12354989"
                },
            };

            var stores = new[]
            {
                new Store
                {
                    Name = "Smart"
                },
                new Store
                {
                    Name = "Bart"
                },
                new Store
                {
                    Name = "Qu"
                },
            };

            var sales = new[]
            {
                new Sale
                {
                    Date = DateTime.Now,
                    Product = products[1],
                    Customer = customers[2],
                    Store = stores[1]
                },
                new Sale
                {
                    Date = DateTime.Now,
                    Product = products[0],
                    Customer = customers[2],
                    Store = stores[1]
                },
                new Sale
                {
                    Date = DateTime.Now,
                    Product = products[0],
                    Customer = customers[1],
                    Store = stores[1]
                },
                new Sale
                {
                    Date = DateTime.Now,
                    Product = products[1],
                    Customer = customers[0],
                    Store = stores[2]
                },
                new Sale
                {
                    Date = DateTime.Now,
                    Product = products[1],
                    Customer = customers[1],
                    Store = stores[0]
                },
                new Sale
                {
                    Date = DateTime.Now,
                    Product = products[1],
                    Customer = customers[1],
                    Store = stores[2]
                },
            };

            using (var db = new SalesContext())
            {
                db.Sales.AddRange(sales);
                db.SaveChanges();
            }
        }
    }
}
