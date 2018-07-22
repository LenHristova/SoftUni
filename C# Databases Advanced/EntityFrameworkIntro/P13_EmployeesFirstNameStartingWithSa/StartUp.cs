using Microsoft.EntityFrameworkCore;

namespace P13_EmployeesFirstNameStartingWithSa
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
                var employeesFirstNameStartingWithSa = db.Employees
                    .Where(e => e.FirstName.StartsWith("Sa"))
                    .OrderBy(e => e.FirstName)
                    .ThenBy(e => e.LastName)
                    .Select(e => $"{e.FirstName} {e.LastName} - {e.JobTitle} - (${e.Salary:F2})");

  
                Console.WriteLine(string.Join(Environment.NewLine, employeesFirstNameStartingWithSa));

                //Case insensetive variant

                //var employeesFirstNameStartingWithSa = db.Employees
                //    .Where(e => EF.Functions.Like(e.FirstName, "sa%"))
                //    .OrderBy(e => e.FirstName)
                //    .ThenBy(e => e.LastName)
                //    .Select(e => $"{e.FirstName} {e.LastName} - {e.JobTitle} - (${e.Salary:F2})");


                //Console.WriteLine(string.Join(Environment.NewLine, employeesFirstNameStartingWithSa));
            }
        }
    }
}
