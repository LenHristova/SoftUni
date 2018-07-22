namespace Employees.App.Commands
{
    using System;
    using Contracts;
    using DtoModels;
    using Employees.Services.Contracts;

    internal class SetAddressCommand : ICommand
    {
        private readonly IEmployeeService employeeService;

        public SetAddressCommand(IEmployeeService employeeService)
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
                throw new ArgumentException("Invalid id format!");
            }

            var address = args[1];

            var employeeDto = this.employeeService.SetAddress<EmployeeDto>(employeeId, address);

            return $"{employeeDto.FirstName} {employeeDto.LastName}'s address was successfuly set to {address}.";
        }
    }
}
