namespace BusTickets.Client.Core.Commands
{
	using System;
	using Dtos;
	using Models;
	using Services.Contracts;

    public class BuyTicketCommand : Command
    {
        public BuyTicketCommand(IRepository repository) 
            : base(repository) { }

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
            var customerDto = this.repository.GetById<Customer, CustomerDto>(customerId);
            this.EnsureNotNull(customerDto, "Customer", customerId.ToString());

            var tripDto = this.repository.GetById<Trip, TripDto>(tripId);
            this.EnsureNotNull(tripDto, "Trip", tripId.ToString());

            var bankAccountDto = this.repository.Get<BankAccount, BankAccountDto>(ba => ba.CustomerId == customerId);
            this.EnsureNotNull(bankAccountDto, "Bank account for customer", customerId.ToString());

            if (bankAccountDto.Balance < price)
            {
                throw new InvalidOperationException($"Insufficient amount of money for customer {customerDto.FirstName} {customerDto.LastName} with bank account number {bankAccountDto.AccountNumber}!");
            }

            bankAccountDto.Balance -= price;
            this.repository.Edit<BankAccount, BankAccountDto>(bankAccountDto);

            var dto = new TicketDto
            {
                Customer = customerDto,
                Trip = tripDto,
                Price = price,
                Seat = seat
            };

            this.repository.Add<Ticket, TicketDto>(dto);

            return $"Customer {customerDto.FirstName} {customerDto.LastName} bought ticket for trip {tripId} for ${price} on seat {seat}!";
        }
    }
}
