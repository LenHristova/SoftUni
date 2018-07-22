namespace P05_EmployeesFromResearchAnd_evelopment
{
    using System;
    using System.Linq;
    using P02_DatabaseFirst.Data;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            using (var db = new SoftUniContext())
            {
                var employees = db.Employees
                    .Where(e => e.Department.Name == "Research and Development")
                    .OrderBy(e => e.Salary)
                    .ThenByDescending(e => e.FirstName)
                    .Select(e => $"{e.FirstName} {e.LastName} from {e.Department.Name} - ${e.Salary:F2}");

                Console.WriteLine(string.Join(Environment.NewLine, employees));
            }
        }
    }
}
