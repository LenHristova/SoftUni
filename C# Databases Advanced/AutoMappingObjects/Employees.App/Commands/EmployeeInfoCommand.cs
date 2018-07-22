namespace Employees.App.Commands
{
    using System;
    using Contracts;
    using DtoModels;
    using Employees.Services.Contracts;

    internal class EmployeeInfoCommand : ICommand
    {
        private readonly IEmployeeService employeeService;

        public EmployeeInfoCommand(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        public string Execute(params string[] args)
        {
            if (args.Length != 1)
            {
                throw new ArgumentException("Invalid parameters count!");
            }

            if (!int.TryParse(args[0], out var employeeId))
            {
                throw new ArgumentException("Invalid id format!");
            }

            var employeeDto = this.employeeService.GetEmployeeInfo<EmployeeDto>(employeeId);

            return employeeDto.ToString();
        }
    }
}
