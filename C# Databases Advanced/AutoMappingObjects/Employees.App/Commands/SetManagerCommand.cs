namespace Employees.App.Commands
{
    using System;
    using System.Linq;
    using Contracts;
    using DtoModels;
    using Employees.Services.Contracts;

    public class SetManagerCommand : ICommand
    {
        private readonly IEmployeeService employeeService;

        public SetManagerCommand(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        public string Execute(params string[] args)
        {
            if (args.Length != 2)
            {
                throw new ArgumentException("Invalid parameters count!");
            }

            if (!int.TryParse(args[0], out var employeeId))
            {
                throw new ArgumentException("Invalid employee's id format!");
            }

            if (!int.TryParse(args[1], out var managerId))
            {
                throw new ArgumentException("Invalid manager's id format!");
            }

            var managerDto = this.employeeService.SetManager<ManagerDto>(employeeId, managerId);

            var employeeDto = managerDto.Employees.First(e => e.Id == employeeId);

            return $"{managerDto.FirstName} {managerDto.LastName} " +
                   $"was successfuly set to be manager to " +
                   $"{employeeDto.FirstName} {employeeDto.LastName}.";
        }
    }
}
