namespace P12_IncreaseSalaries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using P02_DatabaseFirst.Data;

    public class StartUp
    {
        public static void Main()
        {
            var departmentsToIncreaseSalary = new[]
            {
                "Engineering",
                "Tool Design",
                "Marketing",
                "Information Services"
            };

            using (var db = new SoftUniContext())
            {
                var employeesToIncreaseSalary = db.Employees
                    .Where(e => departmentsToIncreaseSalary.Contains(e.Department.Name));

                foreach (var employee in employeesToIncreaseSalary)
                {
                    employee.Salary *= 1.12M;
                }

                db.SaveChanges();

                var orderedEmployeesWithIncreasedSalary = db.Employees
                    .Where(e => departmentsToIncreaseSalary.Contains(e.Department.Name))
                    .OrderBy(e => e.FirstName)
                    .ThenBy(e => e.LastName)
                    .Select(e => $"{e.FirstName} {e.LastName} (${e.Salary:F2})");

                Console.WriteLine(string.Join(Environment.NewLine, orderedEmployeesWithIncreasedSalary));
            }
        }
    }
}
