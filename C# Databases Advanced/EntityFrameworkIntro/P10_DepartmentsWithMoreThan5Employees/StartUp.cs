namespace P10_DepartmentsWithMoreThan5Employees
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
                var departments = db.Departments
                    .Where(d => d.Employees.Count > 5)
                    .OrderBy(d => d.Employees.Count)
                    .ThenBy(d => d.Name)
                    .Select(d => new
                    {
                        BaseInfo = $"{d.Name} - {d.Manager.FirstName} {d.Manager.LastName}",
                        Employees = d.Employees
                            .OrderBy(e => e.FirstName)
                            .ThenBy(e => e.LastName)
                            .Select(e => $"{e.FirstName} {e.LastName} - {e.JobTitle}")
                            .ToArray()
                    })
                    .ToArray();

                foreach (var department in departments)
                {
                    Console.WriteLine(department.BaseInfo);
                    Console.WriteLine(string.Join(Environment.NewLine, department.Employees));
                    Console.WriteLine("----------");
                }
            }
        }
    }
}
