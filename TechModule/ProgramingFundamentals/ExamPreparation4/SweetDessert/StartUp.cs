using System;

namespace SweetDessert
{
    class StartUp
    {
        static void Main()
        {
            double amountCash = double.Parse(Console.ReadLine());
            int guestsCount = int.Parse(Console.ReadLine());
            double bananasPrice = double.Parse(Console.ReadLine());
            double eggsPrice = double.Parse(Console.ReadLine());
            double berriesPrice = double.Parse(Console.ReadLine());

            int dessertsCount = (int) Math.Ceiling(guestsCount / 6.0);

            double bill = dessertsCount * (2 * bananasPrice + 4 * eggsPrice + 0.2 * berriesPrice);

            if (amountCash >= bill)
            {
                Console.WriteLine($"Ivancho has enough money - it would cost {bill:F2}lv.");
            }
            else
            {
                double neededMoney = bill - amountCash;
                Console.WriteLine($"Ivancho will have to withdraw money - he will need {neededMoney:F2}lv more.");
            }
        }
    }
}