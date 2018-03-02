using System;
using System.Globalization;
using P02_MultipleImplementation.Models;

namespace P02_MultipleImplementation
{
    public class StartUp
    {
        private const string DATE_TIME_FORMAT = "dd/MM/yyyy";
        private static readonly CultureInfo DateTimeProvider = CultureInfo.InvariantCulture;

        static void Main()
        {
            var name = Console.ReadLine();
            var age = int.Parse(Console.ReadLine());
            var id = Console.ReadLine();
            var birthdate = DateTime.ParseExact(Console.ReadLine(), DATE_TIME_FORMAT, DateTimeProvider);
            IIdentifiable identifiable = new Citizen(name, age, id, birthdate);
            IBirthable birthable = new Citizen(name, age, id, birthdate);
            Console.WriteLine(identifiable.Id);
            Console.WriteLine(birthable.Birthdate.ToString(DATE_TIME_FORMAT, DateTimeProvider));

        }
    }
}
