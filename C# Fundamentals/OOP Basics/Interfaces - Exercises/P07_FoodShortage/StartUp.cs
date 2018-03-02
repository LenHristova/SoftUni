using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using P07_FoodShortage.Models;

namespace P07_FoodShortage
{
    class StartUp
    {
        private const string DATE_TIME_FORMAT = "dd/MM/yyyy";
        private static readonly CultureInfo DateTimeProvider = CultureInfo.InvariantCulture;

        static void Main()
        {
            var buyers = new List<IBuyer>();
            var buyersCount = int.Parse(Console.ReadLine());
            for (int i = 0; i < buyersCount; i++)
            {
                var buyerInfo = Console.ReadLine().Split();
                AddBuyer(buyerInfo, buyers);
            }

            string input;
            while ((input = Console.ReadLine()) != "End")
            {
                var buyerName = input;
                var currentBuyer = buyers.FirstOrDefault(b => b.Name == buyerName);
                currentBuyer?.BuyFood();
            }

            var perchasedFood = buyers
                .Select(b => b.Food)
                .Sum();

            Console.WriteLine(perchasedFood);
        }

        private static void AddBuyer(IReadOnlyList<string> buyerInfo, ICollection<IBuyer> buyers)
        {
            if (buyerInfo.Count < 3)
            {
                return;
            }

            var name = buyerInfo[0];
            var age = int.Parse(buyerInfo[1]);

            switch (buyerInfo.Count)
            {
                case 4:
                {
                    var id = buyerInfo[2];
                    var birthdate = DateTime.ParseExact(buyerInfo[3], DATE_TIME_FORMAT, DateTimeProvider);

                    buyers.Add(new Citizen(name, age, id, birthdate));
                    break;
                }
                case 3:
                {
                    var group = buyerInfo[2];

                    buyers.Add(new Rebel(name, age, group));
                    break;
                }
            }
        }
    }
}