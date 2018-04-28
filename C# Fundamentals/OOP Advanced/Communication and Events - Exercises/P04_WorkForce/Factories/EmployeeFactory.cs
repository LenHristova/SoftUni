using System;
using System.Linq;
using System.Reflection;

using P04_WorkForce.Contracts;

namespace P04_WorkForce.Factories
{
    public class EmployeeFactory : IEmployeeFactory
    {
        public IEmployee CreateEmployee(string employeeType, string name)
        {
            var type = Assembly.GetExecutingAssembly()
                .GetTypes()
                .FirstOrDefault(t => t.Name == employeeType);

            if (type == null)
            {
                throw new InvalidOperationException($"Invalid employee type: \"{employeeType}\"");
            }

            var args = new object[] { name };
            var employee = (IEmployee)Activator.CreateInstance(type, args);
            return employee;
        }
    }
}
