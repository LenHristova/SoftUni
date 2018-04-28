using System;
using System.Collections.Generic;
using System.Linq;

using P04_WorkForce.Contracts;

namespace P04_WorkForce.Core
{
    public class EmployeeManager : IEmployeeManager
    {
        private readonly ICollection<IEmployee> _employees;
        private readonly IEmployeeFactory _employeeFactory;

        public EmployeeManager(ICollection<IEmployee> employees, IEmployeeFactory employeeFactory)
        {
            _employees = employees;
            _employeeFactory = employeeFactory;
        }

        public void CreateEmployee(params string[] args)
        {
            var employeeType = args[0];
            var employeeName = args[1];
            var employee = _employeeFactory.CreateEmployee(employeeType, employeeName);
            _employees.Add(employee);
        }

        public IEmployee GetEmployee(string employeeName)
        {
            var employee = _employees.FirstOrDefault(e => e.Name == employeeName);

            if (employee == null)
            {
               throw new ArgumentException($"Employee with name \"{employeeName}\" not found!");
            }

            return employee;
        }
    }
}
