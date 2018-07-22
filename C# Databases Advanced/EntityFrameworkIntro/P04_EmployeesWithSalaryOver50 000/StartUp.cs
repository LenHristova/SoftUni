namespace P04_EmployeesWithSalaryOver50_000
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
                    .Where(e => e.Salary > 50000)
                    .Select(e => e.FirstName)
                    .OrderBy(e => e);

                Console.WriteLine(string.Join(Environment.NewLine, employees));
            }
        }
    }
}
