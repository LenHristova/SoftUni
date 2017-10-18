using System;
using System.Collections.Generic;
using System.Linq;

class StartUp
{
    class Sale
    {
        public string Town { get; set; }
        public string Product { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public decimal Bill => Price * Quantity;
    }
    static void Main()
    {
        int salesCount = int.Parse(Console.ReadLine());
        Sale[] allSales = new Sale[salesCount];

        for (int currSale = 0; currSale < salesCount; currSale++)
        {
            string[] saleArgs = Console.ReadLine()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            allSales[currSale] = new Sale
            {
                Town = saleArgs[0],
                Product = saleArgs[1],
                Price = decimal.Parse(saleArgs[2]),
                Quantity = decimal.Parse(saleArgs[3])
            };
        }

        Dictionary<string, decimal> salesByTown = allSales
            .GroupBy(s => s.Town, s => s.Bill)
            .ToDictionary(v => v.Key, v => v.Sum());

        foreach (var town in salesByTown.OrderBy(d => d.Key))
        {
            Console.WriteLine($"{town.Key} -> {town.Value:F2}");
        }
    }
}