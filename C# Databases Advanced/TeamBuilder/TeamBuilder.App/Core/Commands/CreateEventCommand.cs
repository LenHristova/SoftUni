namespace TeamBuilder.App.Core.Commands
{
    using System;
    using System.Globalization;
    using Contracts;
    using Services.Contracts;
    using Utilities;

    public class CreateEventCommand : ICommand
    {
        private readonly IUserService userService;
        private readonly IEventService eventService;

        public CreateEventCommand(IUserService userService, IEventService eventService)
        {
            this.userService = userService;
            this.eventService = eventService;
        }

        // CreateEvent <name> <description> <startDate> <endDate>
        public string Execute(string[] args)
        {
            Check.CheckLength(6, args);

            if (!this.userService.IsAuthenticated())
            {
                throw new InvalidOperationException(Constants.ErrorMessages.LoginFirst);
            }

            var name = args[0];
            ValidateName(name);

            var description = args[1];
            ValidateDescription(description);

            var startDateString = $"{args[2]} {args[3]}";
            var endDateString = $"{args[4]} {args[5]}";

            var isValidStartDate = DateTime.TryParseExact(
                startDateString, Constants.DateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var startDate);
            var isValidEndDate = DateTime.TryParseExact(
                endDateString, Constants.DateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var endDate);

            if (!isValidStartDate || !isValidEndDate)
            {
                throw new ArgumentException(Constants.ErrorMessages.InvalidDateFormat);
            }

            if (startDate > endDate)
            {
                throw new ArgumentException(Constants.ErrorMessages.InvalidTimeSpan);
            }

            var hasDublicate = this.eventService.HasDublicate(name, description, startDate, endDate);
            if (hasDublicate)
            {
                throw new ArgumentException(Constants.ErrorMessages.DublicateEvent);
            }

            this.eventService.CreateEvent(name, description, startDate, endDate, this.userService.GetCurrentUser().Id);

            return string.Format(Constants.SuccessfulMessages.SuccessfulCreatedEvent, name);
        }

        private static void ValidateDescription(string description)
        {
            var isValid = description.Length <= Constants.MaxEventDescriptionLength;

            if (!isValid)
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.EventDescriptionNotValid, description));
            }
        }

        private static void ValidateName(string name)
        {
            var isValid = name.Length <= Constants.MaxEventNameLength;

            if (!isValid)
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.EventNameNotValid, name));
            }
        }
    }
}

