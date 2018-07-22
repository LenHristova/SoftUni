namespace Employees.App.Commands
{
    using System;
    using Contracts;
    using DtoModels;
    using Employees.Services.Contracts;

    public class ListEmployeesOlderThanCommand : ICommand
    {
        private readonly IEmployeeService employeeService;

        public ListEmployeesOlderThanCommand(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        public string Execute(params string[] args)
        {
            if (args.Length != 1)
            {
                throw new ArgumentException("Invalid parameters count!");
            }

            if (!int.TryParse(args[0], out var minAge))
            {
                throw new ArgumentException("Invalid age format!");
            }

            var dtos = this.employeeService.ListEmployeesOlderThan<EmployeeManagerInfoDto>(minAge);

            return string.Join(Environment.NewLine, dtos);
        }
    }
}
