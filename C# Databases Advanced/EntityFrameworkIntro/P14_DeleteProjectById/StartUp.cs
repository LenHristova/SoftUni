using System;
using System.Linq;
using P02_DatabaseFirst.Data;

namespace P14_DeleteProjectById
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            using (var db = new SoftUniContext())
            {
                var employeesProjectsToDelete = db.EmployeesProjects
                    .Where(ep => ep.ProjectId == 2);
                db.EmployeesProjects.RemoveRange(employeesProjectsToDelete);

                var projectToDelete = db.Projects.Find(2);
                db.Projects.Remove(projectToDelete);

                db.SaveChanges();

                var projects = db.Projects
                    .Select(p => p.Name)
                    .Take(10);

                Console.WriteLine(string.Join(Environment.NewLine, projects));
            }
        }
    }
}
