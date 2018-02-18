namespace P06_CompanyRoster
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class StartUp
    {
        static void Main()
        {
            var departmentsEmployees = new Dictionary<string, List<Employee>>();

            var employeesCount = int.Parse(Console.ReadLine());
            for (int employee = 0; employee < employeesCount; employee++)
            {
                var employeeInfo = Console.ReadLine()?.Split();
                if (employeeInfo == null || employeeInfo.Length < 4) continue;

                var name = employeeInfo[0];
                var salary = decimal.Parse(employeeInfo[1]);
                var position = employeeInfo[2];
                var department = employeeInfo[3];

                if (!departmentsEmployees.ContainsKey(department))
                {
                    departmentsEmployees.Add(department, new List<Employee>());
                }

                var currentEmployee = new Employee(name, salary, position, department);

                if (employeeInfo.Length > 4)
                {
                    AddInfo(employeeInfo[4], currentEmployee);
                }
                if (employeeInfo.Length > 5)
                {
                    AddInfo(employeeInfo[5], currentEmployee);
                }

                departmentsEmployees[department].Add(currentEmployee);
            }

            var departmentWithHigherSalary = departmentsEmployees
                .OrderByDescending(d => d.Value.Select(e => e.Salary).Average())
                .FirstOrDefault();

            Console.WriteLine($"Highest Average Salary: {departmentWithHigherSalary.Key}");
            Console.WriteLine(string.Join(Environment.NewLine, departmentWithHigherSalary.Value
                                                                .OrderByDescending(e => e.Salary)));
        }

        private static void AddInfo(string optionalEmployeeInfo, Employee currentEmployee)
        {
            if (int.TryParse(optionalEmployeeInfo, out int age))
            {
                currentEmployee.Age = age;
            }
            else
            {
                var email = optionalEmployeeInfo;
                currentEmployee.Email = email;
            }
        }
    }
}