namespace BusTickets.Services.Contracts
{
	using System;
	using System.Collections.Generic;
	using System.Text;
	
    public interface IBankAccountService
    {
        void Add<TDto>(TDto dto);
    }
}
