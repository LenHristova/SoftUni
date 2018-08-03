namespace BusTickets.Services
{
	using System;
	using System.Collections.Generic;
	using System.Text;
	using Contracts;
	using Models;

    public class CustomerService : ICustomerService
    {
        private readonly IRepository<Customer> customerRepo;

        public CustomerService(IRepository<Customer> customerRepo)
        {
            this.customerRepo = customerRepo;
        }

        public TDto GetById<TDto>(int customerId)
            => this.customerRepo.GetById<TDto>(customerId);
    }
}
