namespace TeamBuilder.Services.Contracts
{
    using System;
    using Models;

    public interface IEventService
    {
        void CreateEvent(string name, string description, DateTime startDate, DateTime endDate, int creatorId);

        bool Exists(string eventName);

        Event ByName(string eventName);

        bool IsUserCreatorOfEvent(int eventId, int userId);

        bool HasDublicate(string eventName, string description, DateTime startDate, DateTime endDate);
    }
}
