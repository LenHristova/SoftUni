namespace TeamBuilder.Services
{
	using Contracts;
	using Data;
	using Models;

    public class TeamEventService : ITeamEventService
    {
        private readonly TeamBuilderContext context;

        public TeamEventService(TeamBuilderContext context)
        {
            this.context = context;
        }

        public void AddTeamEvent(int teamId, int eventId)
        {
            var teamEvent = new TeamEvent
            {
                TeamId = teamId,
                EventId = eventId
            };

            this.context.TeamEvents.Add(teamEvent);
            this.context.SaveChanges();
        }
    }
}
