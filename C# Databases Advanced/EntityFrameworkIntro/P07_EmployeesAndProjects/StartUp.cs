using System.Globalization;

namespace P07_EmployeesAndProjects
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
                    .Where(e => e.EmployeesProjects.Any(ep => ep.Project.StartDate.Year > 2000 && ep.Project.StartDate.Year < 2004))
                    .Take(30)
                    .Select(e => new
                    {
                        Name = $"{e.FirstName} {e.LastName}",
                        ManagerName = $"{e.Manager.FirstName} {e.Manager.LastName}",
                        Projects = e.EmployeesProjects
                            .Select(p => new
                            {
                                p.Project.Name,
                                StartDate = p.Project.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture),
                                EndDate = p.Project.EndDate.HasValue 
                                    ? p.Project.EndDate.Value.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture) 
                                    : "not finished"
                            })
                            .ToArray()
                    });

                foreach (var employee in employees)
                {
                    Console.WriteLine($"{employee.Name} - Manager: {employee.ManagerName}");

                    foreach (var project in employee.Projects)
                    {
                        Console.WriteLine($"--{project.Name} - {project.StartDate} - {project.EndDate}");
                    }
                }
            }
        }
    }
}
