namespace Employees.App.Commands
{
    using System;
    using System.Globalization;
    using Contracts;
    using DtoModels;
    using Employees.Services.Contracts;

    internal class SetBirthdayCommand : ICommand
    {
        private readonly IEmployeeService employeeService;

        public SetBirthdayCommand(IEmployeeService employeeService)
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

            if (!DateTime.TryParseExact(args[1], "d-M-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var birthday))
            {
                throw new ArgumentException("Invalid birthday format!");
            }

            var employeeDto = this.employeeService.SetBirthday<EmployeeDto>(employeeId, birthday);

            return $"{employeeDto.FirstName} {employeeDto.LastName}'s birthday was successfuly set to {birthday:d MMMM yyyy}.";
        }
    }
}
