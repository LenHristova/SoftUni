using System;
using System.Numerics;

namespace AnonymousDownsite
{
    class StartUp
    {
        static void Main()
        {
            int n = int.Parse(Console.ReadLine());
            BigInteger securityKey = BigInteger.Parse(Console.ReadLine());

            decimal totalMoneyLoss = 0m;

            for (int i = 0; i < n; i++)
            {
                string[] websiteData = Console.ReadLine()
                    .Split(new[] {' ', '\t', '\n'}, StringSplitOptions.RemoveEmptyEntries);

                string siteName = websiteData[0];
                decimal siteVisits = decimal.Parse(websiteData[1]);
                decimal siteCommercialPricePerVisit = decimal.Parse(websiteData[2]);

                decimal siteLoss = siteVisits * siteCommercialPricePerVisit;
                totalMoneyLoss += siteLoss;
                Console.WriteLine(siteName);
            }

            BigInteger securityToken = BigInteger.Pow(securityKey, n);

            Console.WriteLine($"Total Loss: {totalMoneyLoss:F20}");
            Console.WriteLine($"Security Token: {securityToken}");
        }
    }
}