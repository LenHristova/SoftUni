namespace BusTickets.Client.Core.Commands
{
	using System;
	using Dtos;
	using Models;
	using Services.Contracts;

    public class DepositCommand : Command
    {
        private readonly IRepository<BankAccount> bankAccountRepository;

        public DepositCommand(IRepository<BankAccount> bankAccountRepository)
        {
            this.bankAccountRepository = bankAccountRepository;
        }

        public override string Execute(string[] data)
        {
            this.EnsureParametersCount(data.Length, 2);

            if (!int.TryParse(data[0], out var bankAccountId))
            {
                throw new ArgumentException("Invalid bank account's id format!");
            }

            if (!decimal.TryParse(data[1], out var amount))
            {
                throw new ArgumentException("Invalid amount format!");
            }

            if (amount <= 0)
            {
                throw new ArgumentException("Amount must be positive!");
            }

            var bankAccountDto = this.bankAccountRepository.GetById<BankAccountDto>(bankAccountId);
            this.EnsureNotNull(bankAccountDto, "Bank account", bankAccountId.ToString());

            bankAccountDto.Balance += amount;
            this.bankAccountRepository.Edit(bankAccountDto);

            return $"Successful deposit (${amount} to bank account {bankAccountId})!";
        }
    }
}
