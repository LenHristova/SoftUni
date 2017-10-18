using System;
using System.Collections.Generic;
using System.Linq;

class StartUp
{
    class Product
    {
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
    }

    class SalesByTown
    {
        public List<Product> Products { get; set; }

        public Decimal Bill()
        {
            return Products.Select(p => p.Quantity * p.Price).Sum();
        }
    }
    static void Main()
    {
        SortedDictionary<string, SalesByTown> salesByTowns = new SortedDictionary<string, SalesByTown>();

        int salesCount = int.Parse(Console.ReadLine());
        for (int currSale = 0; currSale < salesCount; currSale++)
        {
            string[] saleArgs = Console.ReadLine()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            string town = saleArgs[0];
            if (!salesByTowns.ContainsKey(town))
            {
                salesByTowns.Add(town, new SalesByTown
                {
                    Products = new List<Product>()
                });
            }
            salesByTowns[town].Products.Add(new Product
            {
                Price = decimal.Parse(saleArgs[2]),
                Quantity = decimal.Parse(saleArgs[3])
            });
        }

        foreach (var town in salesByTowns)
        {
            Console.WriteLine($"{town.Key} -> {town.Value.Bill():F2}");
        }
    }
}