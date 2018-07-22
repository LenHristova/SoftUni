namespace P06_AddingNewAddressAndUpdatingEmployee
{
    using System;
    using System.Linq;
    using P02_DatabaseFirst.Data;
    using P02_DatabaseFirst.Data.Models;

    public class StartUp
    {
        public static void Main()
        {
            using (var db = new SoftUniContext())
            {
                var adress = new Address
                {
                    AddressText = "Vitoshka 15",
                    TownId = 4
                };

                var employee = db.Employees.FirstOrDefault(e => e.LastName == "Nakov");

                if (employee != null)
                {
                    employee.Address = adress;
                }

                db.SaveChanges();

                var addresses = db.Employees
                    .OrderByDescending(e => e.AddressId)
                    .Take(10)
                    .Select(e => $"{e.Address.AddressText}");

                Console.WriteLine(string.Join(Environment.NewLine, addresses));
            }
        }
    }
}
