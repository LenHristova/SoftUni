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

        bool IsAvailable(int id);

        int? GetTicketsCount(int id);

        bool BuyTickets(int eventId, int orderedTickets);

        int GetCount();
    }
}
