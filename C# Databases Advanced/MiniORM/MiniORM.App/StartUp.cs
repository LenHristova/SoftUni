namespace MiniORM.App
{
    using System;
    using System.Linq;
    using Data;
    using Data.Entities;

    public class StartUp
    {
        public static void Main()
        {
            var context = new SoftUniDbContext(Config.ConnectionString);

            var employeeToDelete = context.Employees.First();
            var employeeProjectsToDelete = employeeToDelete.Projects
                .Where(ep => ep.EmployeeId == employeeToDelete.Id).ToArray();
            context.EmployeesProjects.RemoveRange(employeeProjectsToDelete);
            context.SaveChanges();

            employeeToDelete = context.Employees.First();
            context.Employees.Remove(employeeToDelete);

            //var employeeToDelete = context.Employees.First();
            //var employeeProjectsToDelete = context.EmployeesProjects
            //    .Where(ep => ep.EmployeeId == employeeToDelete.Id).ToArray();
            //context.EmployeesProjects.RemoveRange(employeeProjectsToDelete);

            //context.Employees.Remove(employeeToDelete);


            //context.Employees.Add(new Employee
            //{
            //    FirstName = "Gosho",
            //    LastName = "Inserted",
            //    DepartmentId = context.Departments.First().Id,
            //    IsEmployed = true,
            //});

            //var employee = context.Employees.Last();
            //employee.FirstName = "Modified";

            context.SaveChanges();
        }

        private static void RemoveRangeEmployeeProjects(SoftUniDbContext context)
        {
            var employeeProjectsToDelete = context.EmployeesProjects
                .Where(ep => ep.ProjectId == 1).ToArray();
            context.EmployeesProjects.RemoveRange(employeeProjectsToDelete);
            context.SaveChanges();
        }

        private static void RemoveEmployeeProject(SoftUniDbContext context)
        {
            var employeeProject = context.EmployeesProjects.Last();
            context.EmployeesProjects.Remove(employeeProject);
            context.SaveChanges();
        }

        private static void ModifyEmployee(SoftUniDbContext context)
        {
            var employee = context.Employees.FirstOrDefault(e => e.Id == 1);
            if (employee == null)
            {
                throw new ArgumentException("Employee not found.");
            }

            employee.FirstName = "Modified";
            context.SaveChanges();
        }

        private static void AddEmloyeeProject(SoftUniDbContext context)
        {
            var employeeProject = new EmployeeProject()
            {
                EmployeeId = 2,
                ProjectId = 1
            };

            context.EmployeesProjects.Add(employeeProject);
            context.SaveChanges();
        }

        private static void AddProject(SoftUniDbContext context)
        {
            var project = new Project
            {
                Name = "Python Project"
            };

            context.Projects.Add(project);
            context.SaveChanges();
        }

        private static void AddDepartment(SoftUniDbContext context)
        {
            var deparment = new Department
            {
                Name = "Sales"
            };

            context.Departments.Add(deparment);
            context.SaveChanges();
        }

        private static void AddEmployee(SoftUniDbContext context)
        {
            var employee = new Employee()
            {
                FirstName = "Pesho",
                LastName = "Goshov",
                DepartmentId = context.Departments.First().Id,
                IsEmployed = true
            };

            context.Employees.Add(employee);
            context.SaveChanges();
        }
    }
}
