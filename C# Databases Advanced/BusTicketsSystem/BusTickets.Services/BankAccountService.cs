namespace BusTickets.Services
{
	using System;
	using System.Collections.Generic;
	using System.Text;
	using Contracts;
	using Models;

    public class BankAccountService : IBankAccountService
    {
        private readonly IRepository<BankAccount> bankAccountRepo;

        public BankAccountService(IRepository<BankAccount> bankAccountRepo)
        {
            this.bankAccountRepo = bankAccountRepo;
        }

        public void Add<TDto>(TDto dto)
            => this.bankAccountRepo.Add(dto);
    }
}
