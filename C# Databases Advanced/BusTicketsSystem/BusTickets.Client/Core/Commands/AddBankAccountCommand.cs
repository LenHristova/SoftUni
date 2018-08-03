namespace BusTickets.Client.Core.Commands
{
	using System;
	using Dtos;
	using Services.Contracts;

    public class AddBankAccountCommand : Command
    {
        private readonly ICustomerService customerService;
        private readonly IBankAccountService bankAccountService;

        public AddBankAccountCommand(ICustomerService customerService, IBankAccountService bankAccountService)
        {
            this.customerService = customerService;
            this.bankAccountService = bankAccountService;
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

            var customerDto = this.customerService.GetById<CustomerDto>(customerId);
            this.EnsureNotNull(customerDto, "Customer", customerId.ToString());

            var bankAccountDto = new BankAccountDto
            {
                Customer = customerDto,
                Balance = balance
            };

            this.bankAccountService.Add(bankAccountDto);

            return $"Bank account {customerDto.FirstName} {customerDto.LastName} was added successfully!";
        }
    }
}
