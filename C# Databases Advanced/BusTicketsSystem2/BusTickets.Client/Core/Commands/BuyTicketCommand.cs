namespace BusTickets.Client.Core.Commands
{
	using System;
	using Dtos;
	using Models;
	using Services;
	using Services.Contracts;

    public class BuyTicketCommand : Command
    {
        private readonly IRepository<Customer> customerRepository;
        private readonly IRepository<BankAccount> bankAccountRepository;
        private readonly IRepository<Trip> tripRepository;
        private readonly IRepository<Ticket> ticketRepository;

        public BuyTicketCommand(IRepository<Customer> customerRepository, IRepository<BankAccount> bankAccountRepository, IRepository<Trip> tripRepository, IRepository<Ticket> ticketRepository)
        {
            this.customerRepository = customerRepository;
            this.bankAccountRepository = bankAccountRepository;
            this.tripRepository = tripRepository;
            this.ticketRepository = ticketRepository;
        }

        public override string Execute(string[] data)
        {
            EnsureParametersCount(data.Length, 4);

            if (!int.TryParse(data[0], out var customerId))
            {
                throw new ArgumentException("Invalid customer's id format!");
            }

            if (!int.TryParse(data[1], out var tripId))
            {
                throw new ArgumentException("Invalid trip's id format!");
            }

            if (!decimal.TryParse(data[2], out var price))
            {
                throw new ArgumentException("Invalid price format!");
            }

            if (price <= 0)
            {
                throw new ArgumentException("Price must be positive!");
            }

            var seat = data[3];
            var customerDto = this.customerRepository.GetById<CustomerDto>(customerId);
            this.EnsureNotNull(customerDto, "Customer", customerId.ToString());

            var tripDto = this.tripRepository.GetById<TripDto>(tripId);
            this.EnsureNotNull(tripDto, "Trip", tripId.ToString());

            var bankAccountDto = this.bankAccountRepository.Get<BankAccountDto>(ba => ba.CustomerId == customerId);
            this.EnsureNotNull(bankAccountDto, "Bank account for customer", customerId.ToString());

            if (bankAccountDto.Balance < price)
            {
                throw new InvalidOperationException($"Insufficient amount of money for customer {customerDto.FirstName} {customerDto.LastName} with bank account number {bankAccountDto.AccountNumber}!");
            }

            bankAccountDto.Balance -= price;
            this.bankAccountRepository.Edit(bankAccountDto);

            var dto = new TicketDto
            {
                Customer = customerDto,
                Trip = tripDto,
                Price = price,
                Seat = seat
            };

            this.ticketRepository.Add(dto);

            return $"Customer {customerDto.FirstName} {customerDto.LastName} bought ticket for trip {tripId} for ${price} on seat {seat}!";
        }
    }
}
