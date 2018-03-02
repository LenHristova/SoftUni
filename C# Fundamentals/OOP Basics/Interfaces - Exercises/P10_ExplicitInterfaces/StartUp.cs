using System;
using P10_ExplicitInterfaces.Contracts;
using P10_ExplicitInterfaces.Models;

namespace P10_ExplicitInterfaces
{
    public class StartUp
    {
        static void Main()
        {
            string input;
            while ((input = Console.ReadLine()) != "End")
            {
                var citizenInfo = input?.Split();
                if (citizenInfo == null) continue;

                var name = citizenInfo[0];
                var country = citizenInfo[1];
                var age = int.Parse(citizenInfo[2]);

                var citizen = new Citizen(name, country, age);

                Console.WriteLine(((IPerson)citizen).GetName());
                Console.WriteLine(((IResident)citizen).GetName());
            }
        }
    }
}