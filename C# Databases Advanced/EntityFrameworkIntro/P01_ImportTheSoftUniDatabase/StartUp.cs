namespace P03_EmployeesFullInformation
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
                var employees = db.Employees
                        .OrderBy(e => e.EmployeeId)
                        .Select(e => new
                        {
                            FullName = $"{e.FirstName} {e.LastName} {e.MiddleName}",
                            e.JobTitle,
                            e.Salary
                        });

                foreach (var employee in employees)
                {
                    Console.WriteLine($"{employee.FullName} {employee.JobTitle} {employee.Salary:F2}");
                }
            }
        }
    }
}
