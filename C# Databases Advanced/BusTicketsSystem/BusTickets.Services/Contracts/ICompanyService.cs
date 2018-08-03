namespace BusTickets.Services.Contracts
{
	using System;
	using Models;

    public interface ICompanyService
    {
        TDto Get<TDto>(Func<Company, bool> func);
    }
}
