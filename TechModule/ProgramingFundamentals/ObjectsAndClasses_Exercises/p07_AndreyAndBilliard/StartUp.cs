using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class StartUp
{
    static void Main()
    {
        Dictionary<string, decimal> menu = GetProducts();

        SortedDictionary<string, Dictionary<string, int>> clientsOrders = GetOrders(menu);

        decimal totalBill = 0L;
        foreach (var client in clientsOrders)
        {
            Console.WriteLine(client.Key);
            decimal currBill = 0L;
            foreach (var order in client.Value)
            {
                Console.WriteLine($"-- {order.Key} - {order.Value}");
                currBill += menu[order.Key] * order.Value;
            }
            Console.WriteLine($"Bill: {currBill:F2}");
            totalBill += currBill;
        }
        Console.WriteLine($"Total bill: {totalBill:F2}");
    }

    private static SortedDictionary<string, Dictionary<string, int>> GetOrders(Dictionary<string, decimal> menu)
    {
        SortedDictionary<string, Dictionary<string, int>> clientsOrders = new SortedDictionary<string, Dictionary<string, int>>();

        string input = Console.ReadLine();
        while (input != "end of clients")
        {
            string[] orderInfo = input
                .Split(new[] {'-', ','}, StringSplitOptions.RemoveEmptyEntries);

            string clientName = orderInfo[0];
            string orderedProduct = orderInfo[1];

            if (menu.ContainsKey(orderedProduct))
            {
                int productQuantity = int.Parse(orderInfo[2]);

                if (!clientsOrders.ContainsKey(clientName))
                {
                    clientsOrders.Add(clientName, new Dictionary<string, int> { });
                }
                if (!clientsOrders[clientName].ContainsKey(orderedProduct))
                {
                    clientsOrders[clientName].Add(orderedProduct, productQuantity);
                }
                else
                {
                    clientsOrders[clientName][orderedProduct] += productQuantity;
                }
            }
            input = Console.ReadLine();
        }

        return clientsOrders;
    }


    private static Dictionary<string, decimal> GetProducts()
    {
        Dictionary<string, decimal> productsPrice = new Dictionary<string, decimal>();

        int productsCount = int.Parse(Console.ReadLine());
        for (int currProduct = 0; currProduct < productsCount; currProduct++)
        {
            string[] productParams = Console.ReadLine()
                .Split(new[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
            string product = productParams[0];
            decimal price = decimal.Parse(productParams[1]);
            if (!productsPrice.ContainsKey(product))
            {
                productsPrice.Add(product, price);
            }
            else
            {
                productsPrice[product] = price;
            }
        }
        return productsPrice;
    }
}