namespace P15_RemoveTowns
{
    using System;
    using System.Linq;
    using P02_DatabaseFirst.Data;

    public class StartUp
    {
        public static void Main()
        {
            var townName = Console.ReadLine();

            using (var db = new SoftUniContext())
            {
                db.Employees
                    .Where(e => e.Address.Town.Name == townName)
                    .ToList()
                    .ForEach(e => e.Address = null);

                var addressesToDelete = db.Addresses
                    .Where(a => a.Town.Name == townName)
                    .ToList();

                db.Addresses.RemoveRange(addressesToDelete);

                var townToDelete = db.Towns
                    .SingleOrDefault(t => t.Name == townName);

                if (townToDelete != null) db.Towns.Remove(townToDelete);

                db.SaveChanges();

                var result = addressesToDelete.Count == 1
                    ? $"{addressesToDelete.Count} address in {townName} was deleted"
                    : $"{addressesToDelete.Count} addresess in {townName} were deleted";
                Console.WriteLine(result);
            }
        }
    }
}
