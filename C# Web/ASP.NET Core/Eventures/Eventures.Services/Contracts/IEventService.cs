namespace Eventures.Services.Contracts
{
    using Common.Models.Events;
    using Data.Models;
    using System.Collections.Generic;

    public interface IEventService
    {
        IEnumerable<TModel> All<TModel>();

        Event Create(CreateEventInputModel model);

        Event GetLastAdded();

        bool Exists(int id);

        int GetTicketsCount(int id);

        bool GetTickets(int id, int ticketsCount);
    }
}
