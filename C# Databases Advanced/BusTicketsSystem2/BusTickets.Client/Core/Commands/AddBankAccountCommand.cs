namespace BusTickets.Client.Core.Commands
{
	using System;
	using Dtos;
	using Models;
	using Services.Contracts;

    public class AddBankAccountCommand : Command
    {
        private readonly IRepository<Customer> customerRepository;
        private readonly IRepository<BankAccount> banRepository;

        public AddBankAccountCommand(IRepository<Customer> customerRepository, IRepository<BankAccount> banRepository)
        {
            this.customerRepository = customerRepository;
            this.banRepository = banRepository;
        }

        public override string Execute(string[] data)
        {
            this.EnsureParametersCount(data.Length, 2);

            if (!int.TryParse(data[0], out var customerId))
            {
                throw new ArgumentException("Invalid id format!");
            }

            if (!decimal.TryParse(data[1], out var balance))
            {
                throw new ArgumentException("Invalid balance format!");
            }

            var customerDto = this.customerRepository.GetById<CustomerDto>(customerId);
            this.EnsureNotNull(customerDto, "Customer", customerId.ToString());

            var bankAccountDto = new BankAccountDto
            {
                Customer = customerDto,
                Balance = balance
            };

            this.banRepository.Add(bankAccountDto);

            return $"Bank account {customerDto.FirstName} {customerDto.LastName} was added successfully!";
        }
    }
}
