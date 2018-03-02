using System;
using P04_Telephony.Models;

namespace P04_Telephony
{
    class StartUp
    {
        static void Main()
        {
            var smartphone = new Smartphone();

            var phoneNumbers = Console.ReadLine().Split();
            foreach (var phoneNumber in phoneNumbers)
            {
                Console.WriteLine(smartphone.Call(phoneNumber));
            }

            var webSites = Console.ReadLine().Split();
            foreach (var webSite in webSites)
            {
                Console.WriteLine(smartphone.Browse(webSite));
            }
        }
    }
}