namespace IRunes.Web.Common
{
    public class Constants
    {
        public const string AuthenticationCookieKey = ".auth-IRunes";

        public class ValidationErrorMessages
        {
            public const string StringLength = "{0} must be with a minimum length of {2} and a maximum length of {1}.";
            public const string LettersAndDigitsOnly = "{0} must contains only letters and digits.";
        }
    }
}
