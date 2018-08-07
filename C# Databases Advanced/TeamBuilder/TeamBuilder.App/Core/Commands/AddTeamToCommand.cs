namespace TeamBuilder.App.Core.Commands
{
	using System;
	using System.Linq;
	using Contracts;
	using Services.Contracts;
	using Utilities;

    public class AddTeamToCommand : ICommand
    {
        private readonly IUserService userService;
        private readonly ITeamService teamService;
        private readonly IEventService eventService;
        private readonly ITeamEventService teamEventService;

        public AddTeamToCommand(
            IUserService userService, 
            ITeamService teamService, 
            IEventService eventService, 
            ITeamEventService teamEventService)
        {
            this.userService = userService;
            this.teamService = teamService;
            this.eventService = eventService;
            this.teamEventService = teamEventService;
        }

        //	AddTeamTo <eventName> <teamName>
        public string Execute(string[] args)
        {
            Check.CheckLength(2, args);

            if (!this.userService.IsAuthenticated())
            {
                throw new InvalidOperationException(Constants.ErrorMessages.LoginFirst);
            }

            var eventName = args[0];
            var teamName = args[1];

            var eventExists = this.eventService.Exists(eventName);
            if (!eventExists)
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.EventNotFound, eventName));
            }

            var teamExists = this.teamService.Exists(teamName);
            if (!teamExists)
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.TeamNotFound, teamName));
            }

            var loggedInUserId = this.userService.GetCurrentUser().Id;
            var @event = this.eventService.ByName(eventName);

            var isCreator = this.eventService.IsUserCreatorOfEvent(@event.Id, loggedInUserId);
            if (!isCreator)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.NotAllowed);
            }

            var teamId = this.teamService.ByName(teamName).Id;
            var isTeamAdded = @event.ParticipatingTeams.Any(t => t.TeamId == teamId);
            if (isTeamAdded)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.CannotAddSameTeamTwice);
            }

            this.teamEventService.AddTeamEvent(teamId, @event.Id);

            return string.Format(Constants.SuccessfulMessages.SuccessfulAddedTeamEvent, teamName, eventName);
        }
    }
}
