namespace CameraBazar.Web.Infrastructure
{
    public class Constants
    {
        public class ValidationErrors
        {
            public const string StringLength = "The {0} must be at least {2} and at max {1} characters long.";
            public const string PasswordConfirmation = "The passwords do not match.";
            public const string LettersOnly = "The {0} must contains only letters.";
            public const string LowercaseLettersAndDigitsOnly = "The {0} must contains only lowercase letters and digits.";
            public const string UppercaseLettersDigitsAndDashOnly = "The {0} must contains only uppercase letters, digits and \"-\".";
            public const string Phone = "The {0} must start with \"+\" sign, followed by 10 to 12 digits.";
            public const string DividedBy100 = "The {0} must be dividable by 100.";
            public const string Url = "The {0} must start with \"http://\" or \"https://\".";
        }
    }
}
