namespace Eventures.Services
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Common.Models.Events;
    using Contracts;
    using Data;
    using Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class EventService : IEventService
    {
        private readonly EventuresDbContext db;
        private readonly IMapper mapper;

        public EventService(EventuresDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public IEnumerable<TModel> All<TModel>()
            => this.db.Events
                .Where(e => e.End >= DateTime.UtcNow && e.TotalTickets > 0)
                .OrderBy(e => e.Start)
                .ProjectTo<TModel>(mapper.ConfigurationProvider)
                .ToList();

        public Event Create(CreateEventInputModel model)
        {
            var @event = this.mapper.Map<Event>(model);

            this.db.Events.Add(@event);
            this.db.SaveChanges();

            return @event;
        }

        public Event GetLastAdded() => this.db.Events.LastOrDefault();

        public bool IsAvailable(int id)
            => this.db.Events
                .Any(e => e.Id == id &&
                          e.End >= DateTime.UtcNow &&
                          e.TotalTickets > 0);

        public int? GetTicketsCount(int id)
        => this.db.Events.FirstOrDefault(e => e.Id == id)?.TotalTickets;

        public bool BuyTickets(int eventId, int orderedTickets)
        {
            if (orderedTickets <= 0)
            {
                return false;
            }

            var @event = this.db.Events.Find(eventId);

            if (@event == null || @event.TotalTickets < orderedTickets)
            {
                return false;
            }

            @event.TotalTickets -= orderedTickets;
            return true;
        }

        public int GetCount() => this.db.Events.Count();
    }
}
