namespace BusTickets.Client.Core.Commands
{
	using System;
	using System.Globalization;
	using Dtos;
	using Models;
	using Models.Enums;
	using Services.Contracts;

    public class AddCustomerCommand : Command
    {
        private readonly IRepository<Customer> customerRepository;

        public AddCustomerCommand(IRepository<Customer> customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public override string Execute(string[] data)
        {
            this.EnsureParametersCount(data.Length, 4);

            var firstName = data[0];
            var lastName = data[1];

            if (!DateTime.TryParseExact(data[2], "d-M-yyyy" , CultureInfo.InvariantCulture, DateTimeStyles.None, out var birthDate))
            {
                throw new ArgumentException("Invalid date of birth format!");
            }

            if (!Enum.TryParse(data[3], out Gender gender))
            {
                throw new ArgumentException("Invalid gender!");
            }

            var customerDto = new CustomerDto
            {
                FirstName = firstName,
                LastName = lastName,
                DateOfBirth = birthDate.ToString("d MMM yyyy"),
                Gender = gender.ToString()
            };

            this.customerRepository.Add(customerDto);

            return $"Customer {firstName} {lastName} was added successfully!";
        }
    }
}
