namespace Employees.App.Commands
{
    using System;
    using Contracts;
    using DtoModels;
    using Employees.Services.Contracts;

    internal class AddEmployeeCommand : ICommand
    {
        private readonly IEmployeeService employeeService;

        public AddEmployeeCommand(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        public string Execute(params string[] args)
        {
            if (args.Length != 3)
            {
                throw new ArgumentException("Invalid parameters count!");
            }

            var firstName = args[0];
            var lastName = args[1];
            if (!decimal.TryParse(args[2], out var salary))
            {
                throw new ArgumentException("Invalid salary format!");
            }

            var employeeDto = new EmployeeDto(firstName, lastName, salary);

            this.employeeService.AddEmployee(employeeDto);

            return $"Employee {firstName} {lastName} with salary {salary} was successfully added.";
        }
    }
}
