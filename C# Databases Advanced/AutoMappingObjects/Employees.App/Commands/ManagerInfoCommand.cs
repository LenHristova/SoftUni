namespace Employees.App.Commands
{
    using System;
    using Contracts;
    using DtoModels;
    using Employees.Services.Contracts;

    public class ManagerInfoCommand : ICommand
    {
        private readonly IEmployeeService employeeService;

        public ManagerInfoCommand(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        public string Execute(params string[] args)
        {
            if (args.Length != 1)
            {
                throw new ArgumentException("Invalid parameters count!");
            }

            if (!int.TryParse(args[0], out var managerId))
            {
                throw new ArgumentException("Invalid id format!");
            }

            var managerDto = this.employeeService.GetEmployeeInfo<ManagerDto>(managerId);

            return managerDto.EmployeesCount > 0
            ? managerDto.ToString()
                : $"Employee with id {managerId} ({managerDto.FirstName} {managerDto.LastName}) is not a manager.";
        }
    }
}
