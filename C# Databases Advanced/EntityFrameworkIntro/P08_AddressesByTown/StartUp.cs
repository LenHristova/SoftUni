namespace P08_AddressesByTown
{
    using System;
    using System.Linq;
    using P02_DatabaseFirst.Data;

    public class StartUp
    {
        public static void Main()
        {
            using (var db = new SoftUniContext())
            {
                var addresses = db.Addresses
                    .OrderByDescending(a => a.Employees.Count)
                    .ThenBy(a => a.Town.Name)
                    .ThenBy(a => a.AddressText)
                    .Take(10)
                    .Select(a => $"{a.AddressText}, {a.Town.Name} - {a.Employees.Count} employees")
                    .ToArray();

                Console.WriteLine(string.Join(Environment.NewLine, addresses));
            }
        }
    }
}
