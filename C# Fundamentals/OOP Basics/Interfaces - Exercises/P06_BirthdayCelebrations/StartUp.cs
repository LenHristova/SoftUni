using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using P06_BirthdayCelebrations.Models;

namespace P06_BirthdayCelebrations
{
    public class StartUp
    {
        private const string DATE_TIME_FORMAT = "dd/MM/yyyy";
        private static readonly CultureInfo DateTimeProvider = CultureInfo.InvariantCulture;

        static void Main()
        {
            var birthbaleInhabitants = new List<IBirthable>();

            string input;
            while ((input = Console.ReadLine()) != "End")
            {
                var inhabitantInfo = input.Split();
                AddBirthableInhabitant(inhabitantInfo, birthbaleInhabitants);
            }

            var year = int.Parse(Console.ReadLine());

            var inhabitantsBornInSearchedYear = birthbaleInhabitants
                .Where(i => i.Birthdate.Year == year)
                .Select(i => i.Birthdate.ToString(DATE_TIME_FORMAT, DateTimeProvider));

            Console.WriteLine(string.Join(Environment.NewLine, inhabitantsBornInSearchedYear));
        }

        private static void AddBirthableInhabitant(IReadOnlyList<string> inhabitantInfo, ICollection<IBirthable> birthbaleInhabitants)
        {
            switch (inhabitantInfo[0])
            {
                case nameof(Citizen):
                {
                    var name = inhabitantInfo[1];
                    var age = int.Parse(inhabitantInfo[2]);
                    var id = inhabitantInfo[3];
                    var birthdate = DateTime.ParseExact(inhabitantInfo[4], DATE_TIME_FORMAT, DateTimeProvider);
                    birthbaleInhabitants.Add(new Citizen(id, name, age, birthdate));
                    break;
                }
                case nameof(Pet):
                {
                    var name = inhabitantInfo[1];
                    var birthdate = DateTime.ParseExact(inhabitantInfo[2], DATE_TIME_FORMAT, DateTimeProvider);
                    birthbaleInhabitants.Add(new Pet(name, birthdate));
                    break;
                }
            }
        }
    }
}