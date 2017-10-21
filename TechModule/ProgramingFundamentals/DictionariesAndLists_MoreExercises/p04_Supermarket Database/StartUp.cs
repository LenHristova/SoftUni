using System;
using System.Collections.Generic;
using System.Linq;

namespace p04_Supermarket_Database
{
    class Product
    {
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public decimal Amount => Price * Quantity;

        public override string ToString()
        {
            return $"${Price} * {Quantity} = ${Amount}";
        }
    }

    class Store
    {
        public Dictionary<string, Product> Products { get; set; } = new Dictionary<string, Product>();
        public decimal Amount => Products.Values
            .Select(v => v.Amount).Sum();

        public override string ToString()
        {
            List<string> productList = new List<string>();
            foreach (var product in Products)
            {
                productList.Add($"{product.Key}: {product.Value}");
            }
            return  $"{string.Join(Environment.NewLine, productList)}" + Environment.NewLine +
                    "------------------------------" + Environment.NewLine +
                    $"Grand Total: ${Amount}";
        }
    }

    class StartUp
    {
        static void Main(string[] args)
        {
            Store store = new Store();

            while (true)
            {
                string input = Console.ReadLine();
                if (input == "stocked")
                    break;

                string[] productsProps = input
                    .Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);

                string productName = productsProps[0];
                decimal productPrice = decimal.Parse(productsProps[1]);
                decimal productQuantity = decimal.Parse(productsProps[2]);

                if (!store.Products.ContainsKey(productName))
                {
                    store.Products[productName] = new Product();
                }

                store.Products[productName].Price = productPrice;
                store.Products[productName].Quantity += productQuantity;
            }

            Console.WriteLine(store);
        }
    }
}
