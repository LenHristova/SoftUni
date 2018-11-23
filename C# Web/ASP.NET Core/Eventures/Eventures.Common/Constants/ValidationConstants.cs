namespace Eventures.Common.Constants
{
    public class ValidationConstants
    {
        public const string StringLength = "The {0} must be at least {2} and at max {1} characters long.";

        public const string PasswordConfirmation = "The password and confirmation password do not match.";

        public const string UsernameRegex = "{0} must contains only	alphanumeric characters, dashes, underscores, dots, asterisks and tildes.";

        public const string UniqueCitizenNumber = "Unique Citizen Number (UCN) must be exact 10 digits.";

        public const string StartDateTime = "Start date and time must be after current date and time.";

        public const string EndDateTime = "End date and time must be after start date and time.";
    }
}
