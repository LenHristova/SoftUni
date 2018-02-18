using System;

namespace P05_GreedyTimes
{
    public class StartUp
    {
        static void Main()
        {
            long bagMaxCapacity = long.Parse(Console.ReadLine());
            var bag = new Bag(bagMaxCapacity);

            string[] items = Console.ReadLine()?
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (items != null)
                for (int i = 0; i < items.Length; i += 2)
                {
                    long amount = long.Parse(items[i + 1]);
                    string item = items[i];
                    bag.TryToAddItemAmount(item, amount);
                }

            Console.WriteLine(bag);
        }
    }
}