namespace BusTickets.Services.Contracts
{
	using System;
	using System.Collections.Generic;
	using System.Text;
	using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;

    public interface ICustomerService
    {
        TDto GetById<TDto>(int customerId);
    }
}
