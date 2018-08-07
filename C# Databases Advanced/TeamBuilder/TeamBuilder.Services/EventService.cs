namespace TeamBuilder.Services
{
    using System;
    using System.Linq;
    using Contracts;
    using Data;
    using Models;

    public class EventService : IEventService
    {
        private readonly TeamBuilderContext context;

        public EventService(TeamBuilderContext context)
        {
            this.context = context;
        }

        public bool Exists(string eventName)
            => this.ByName(eventName) != null;

        public Event ByName(string eventName)
            => this.context.Events
                   .Where(e => e.Name == eventName)
                   .OrderByDescending(e => e.StartDate)
                   .FirstOrDefault();

        public void CreateEvent(string name, string description, DateTime startDate, DateTime endDate, int creatorId)
        {
            var @event = new Event()
            {
                Name = name,
                Description = description,
                StartDate = startDate,
                EndDate = endDate,
                CreatorId = creatorId
            };

            this.context.Events.Add(@event);
            this.context.SaveChanges();
        }

        public bool IsUserCreatorOfEvent(int eventId, int userId)
            => this.context.Events
                .Any(e => e.Id == eventId &&
                          e.CreatorId == userId);

        public bool HasDublicate(string eventName, string description, DateTime startDate, DateTime endDate)
            => this.context.Events
                .Any(e => e.Name == eventName &&
                          e.Description == description &&
                          e.StartDate == startDate &&
                          e.EndDate == endDate);
    }
}
