using System;
using System.Text.RegularExpressions;

namespace SoftuniCoffeeOrders
{
    class StartUp
    {
        static void Main()
        {
            int ordersCount = int.Parse(Console.ReadLine());

            decimal totalPrice = 0L;
            for (int i = 0; i < ordersCount; i++)
            {
                decimal pricePerCapsule = decimal.Parse(Console.ReadLine());

                Match date = Regex.Match(Console.ReadLine(), 
                    @"\d{1,2}\/(?<month>\d{1,2})\/(?<year>\d{4})");

                int capsulesCount = int.Parse(Console.ReadLine());

                int year = int.Parse(date.Groups["year"].Value);
                int month = int.Parse(date.Groups["month"].Value); 

                int daysInMonth = DateTime.DaysInMonth(year, month);

                decimal orderPrice = (decimal)daysInMonth * capsulesCount * pricePerCapsule;
                Console.WriteLine($"The price for the coffee is: ${orderPrice:F2}");

                totalPrice += orderPrice;
            }

            Console.WriteLine($"Total: ${totalPrice:F2}");
        }
    }
}