using System;

namespace p11_PriceChangeAlert
{
    class Program
    {
        static void Main(string[] args)
        {
            int numberOfPrices = int.Parse(Console.ReadLine());
            double significanceThreshold = double.Parse(Console.ReadLine());
            double lastPrice = double.Parse(Console.ReadLine());

            for (int i = 0; i < numberOfPrices - 1; i++)
            {
                double newPrice = double.Parse(Console.ReadLine());

                double diff = GetDiff(lastPrice, newPrice);
                bool isSignificantDifference = IsDiffBiggerThenSignificanceThreshold(
                    diff, significanceThreshold);
                string messageOfTheChanges = GetConditionsOfTheChanges(
                    newPrice, lastPrice, diff, isSignificantDifference);
                Console.WriteLine(messageOfTheChanges);
                lastPrice = newPrice;
            }
        }

        static string GetConditionsOfTheChanges(
            double newPrice, double lastPrice, double diff, bool isSignificantDifference)
        {
            string messageOfTheChanges = String.Empty;
            if (diff == 0)
            {
                messageOfTheChanges = string.Format("NO CHANGE: {0}", newPrice);
            }
            else if (!isSignificantDifference)
            {
                messageOfTheChanges = string.Format("MINOR CHANGE: {0} to {1} ({2:F2}%)",
                    lastPrice, newPrice, diff * 100);
            }
            else if (isSignificantDifference && (diff > 0))
            {
                messageOfTheChanges = string.Format("PRICE UP: {0} to {1} ({2:F2}%)",
                    lastPrice, newPrice, diff * 100);
            }
            else if (isSignificantDifference && (diff < 0))
            {
                messageOfTheChanges = string.Format("PRICE DOWN: {0} to {1} ({2:F2}%)",
                    lastPrice, newPrice, diff * 100);
            }
            return messageOfTheChanges;
        }

        static bool IsDiffBiggerThenSignificanceThreshold(double diff, double significanceThreshold)
        {
            if (Math.Abs(diff) >= significanceThreshold)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static double GetDiff(double lastPrice, double newPrice)
        {
            double diff = (newPrice - lastPrice) / lastPrice;
            return diff;
        }
    }
}
