using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Order
{
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public decimal CurrentBill => this.Quantity * this.Price;
}

class StartUp
{
    static void Main()
    {
        Dictionary<string, decimal> menu = GetMenu();
        SortedDictionary<string, Dictionary<string, List<Order>>> clientOrders =
            GetClientOrders(menu);
        PrintBill(clientOrders);
    }

    private static void PrintBill(
        SortedDictionary<string, Dictionary<string, List<Order>>> clientOrders)
    {
        decimal totalBill = 0;
        foreach (var pair in clientOrders)
        {
            string clientName = pair.Key;
            Console.WriteLine(clientName);

            decimal clientBill = 0;
            Dictionary<string, List<Order>> orderedProducts = pair.Value;
            foreach (var order in orderedProducts)
            {
                string productName = order.Key;
                int quantity = order.Value.Select(x => x.Quantity).Sum();
                Console.WriteLine($"-- {productName} - {quantity}");
                clientBill += order.Value.Select(x => x.CurrentBill).Sum();
            }
            Console.WriteLine($"Bill: {clientBill:F2}");
            totalBill += clientBill;
        }
        Console.WriteLine($"Total bill: {totalBill:F2}");
    }

    private static SortedDictionary<string, Dictionary<string, List<Order>>> GetClientOrders(
        Dictionary<string, decimal> menu)
    {
        var clientOrders = new SortedDictionary<string, Dictionary<string, List<Order>>>();

        while (true)
        {
            string tokens = Console.ReadLine();
            if (tokens == "end of clients")
            {
                break;
            }

            string[] order = tokens.Split(new[] { ',', '-' }).ToArray();
            string clientName = string.Join("", order.Take(1));
            string product = string.Join("", order.Skip(1).Take(1));
            if (!menu.ContainsKey(product))
            {
                continue;
            }
            int quantity = int.Parse(string.Join("", order.Skip(2)));

            if (!clientOrders.ContainsKey(clientName))
            {
                clientOrders[clientName] = new Dictionary<string, List<Order>>();
            }
            if (!clientOrders[clientName].ContainsKey(product))
            {
                clientOrders[clientName][product] = new List<Order>();
            }
            clientOrders[clientName][product].Add(new Order
            {
                Price = menu[product],
                Quantity = quantity
            });
        }
        return clientOrders;
    }

    private static Dictionary<string, decimal> GetMenu()
    {
        Dictionary<string, decimal> menu = new Dictionary<string, decimal>();
        int productCount = int.Parse(Console.ReadLine());
        for (int i = 0; i < productCount; i++)
        {
            string[] product = Console.ReadLine().Split('-');
            string productName = product[0];
            decimal productPrice = decimal.Parse(product[1]);

            if (!menu.ContainsKey(productName))
            {
                menu[productName] = 0;
            }
            menu[productName] = productPrice;
        }

        return menu;
    }
}