namespace TeamBuilder.App.Core.Commands
{
	using System;
	using System.Text;
	using Contracts;
	using Services.Contracts;
	using Utilities;

    public class ShowEventCommand : ICommand
    {
        private readonly IEventService eventService;

        public ShowEventCommand(IEventService eventService)
        {
            this.eventService = eventService;
        }

        //	ShowEvent <eventName>
        public string Execute(string[] args)
        {
            Check.CheckLength(1, args);

            var eventName = args[0];

            var isExists = this.eventService.Exists(eventName);
            if (!isExists)
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.EventNotFound, eventName));
            }

            return GetEventInfo(eventName);
        }

        private string GetEventInfo(string eventName)
        {
            var @event = this.eventService.ByName(eventName);
            var eventDescription = @event?.Description ?? null;
            var formatedStartDate = @event.StartDate.ToString(Constants.DateTimeFormat);
            var formatedEndtDate = @event.EndDate.ToString(Constants.DateTimeFormat);

            var sb = new StringBuilder();
            sb.AppendLine($"{@event.Name} {formatedStartDate} {formatedEndtDate}")
                .AppendLine(eventDescription)
                .AppendLine("Teams:");

            foreach (var team in @event.ParticipatingTeams)
            {
                sb.AppendLine($"  -{team.Team.Name}");
            }

           return sb.ToString().Trim();
        }
    }
}
