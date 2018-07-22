using System;
using System.Linq;
using P02_DatabaseFirst.Data;

namespace P09_Employee147
{
    public class StartUp
    {
        public static void Main()
        {
            using (var db = new SoftUniContext())
            {
                var employee = db.Employees
                    .Where(e => e.EmployeeId == 147)
                    .Select(e => new
                    {
                        BaseInfo = $"{e.FirstName} {e.LastName} - {e.JobTitle}",
                        Projects = e.EmployeesProjects
                            .Select(ep => ep.Project.Name)
                            .OrderBy(p => p) 
                    })
                    .First();

                Console.WriteLine(employee.BaseInfo);
                Console.WriteLine(string.Join(Environment.NewLine, employee.Projects));
            }
        }
    }
}
