namespace BusTickets.Services.Contracts
{
	using System;
	using System.Collections.Generic;
	using System.Text;
	using Models;

    public interface ICountryService
    {
        TDto Get<TDto>(Func<Country, bool> func);
    }
}
