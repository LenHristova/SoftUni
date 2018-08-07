namespace TeamBuilder.App.Utilities
{
    public static class Constants
    {
        public const int MinUsernameLength = 3;
        public const int MaxUsernameLength = 25;

        public const int MaxFirstNameLength = 25;

        public const int MaxLastNameLength = 25;

        public const int MaxEventNameLength = 25;
        public const int MaxEventDescriptionLength = 250;

        public const int MaxTeamNameLength = 25;
        public const int MaxTeamDescriptionLength = 32;
        public const int TeamAcronymLength = 3;

        public const int MinPasswordLength = 6;
        public const int MaxPasswordLength = 30;

        public const string DateTimeFormat = "dd/MM/yyyy HH:mm";

        public static class SuccessfulMessages
        {
            public const string SuccessfulRegistration = "User {0} was registered successfully!";

            public const string SuccessfulLogin = "User {0} successfully logged in!";

            public const string SuccessfulLogout = "User {0} successfully logged out!";

            public const string SuccessfulDeleteUser = "User {0} was deleted successfully!";

            public const string SuccessfulCreatedEvent = "Event {0} was created successfully!";

            public const string SuccessfulCreatedTeam = "Team {0} successfully created!";

            public const string SuccessfulInvateToTeam = "Team {0} invited {1}!";

            public const string SuccessfulAcceptInvite = "User {0} joined team {1}!";

            public const string SuccessfulDeclineInvite = "Invite from {0} declined!";

            public const string SuccessfulDisbandTeam = "{0} has disbanded!";

            public const string SuccessfulKickMember = "User {0} was kicked from {1}!";

            public const string SuccessfulAddedTeamEvent = "Team {0} added for {1}!";
        }

        public static class ErrorMessages
        {
            // Common error messages.
            public const string InvalidArgumentsCount = "Invalid arguments count!";

            public const string LogoutFirst = "You should logout first!";
            public const string LoginFirst = "You should login first!";

            public const string TeamOrUserNotExist = "Team or user does not exist!";
            public const string InviteIsAlreadySent = "Invite is already sent!";

            public const string NotAllowed = "Not allowed!";

            public const string TeamNotFound = "Team {0} not found!";
            public const string UserNotFound = "User {0} not found!";
            public const string EventNotFound = "Event {0} not found!";
            public const string InviteNotFound = "Invite from {0} is not found!";

            public const string NotPartOfTeam = "User {0} is not a member in {1}!";

            public const string CommandNotAllowed = "Command not allowed. Use {0} instead.";
            public const string CannotAddSameTeamTwice = "Cannot add same team twice!";

            // User error messages.
            public const string UsernameNotValid = "Username {0} not valid!";
            public const string PasswordNotValid = "Password {0} not valid!";
            public const string FirstNameNotValid = "First name {0} not valid!";
            public const string LastNameNotValid = "Last name {0} not valid!";
            public const string PasswordDoesNotMatch = "Passwords do not match!";
            public const string AgeNotValid = "Age not valid!";
            public const string GenderNotValid = "Gender should be either “Male” or “Female”!";
            public const string UsernameIsTaken = "Username {0} is already taken!";
            public const string UserOrPasswordIsInvalid = "Invalid username or password!";

            public const string InvalidDateFormat = "Please insert the dates in format: [dd/MM/yyyy HH:mm]!";
            public const string InvalidTimeSpan = "Start date should be before end date!";

            // Event error messages.
            public const string EventNameNotValid = "Event name {0} not valid!";
            public const string EventDescriptionNotValid = "Event description {0} not valid!";
            public const string DublicateEvent = "Event with the same name, description, start date and end date already exists!";

            // Team error messages.
            public const string TeamNameNotValid = "Team name {0} not valid!";
            public const string TeamDescriptionNotValid = "Team description {0} not valid!";
            public const string TeamAcronymNotValid = "Team acronym {0} not valid!";
            public const string TeamExists = "Team {0} exists!";
        }
    }
}
