namespace CarDealer.Web.Infrastructure
{
    public class Constants
    {
        public const string NotFoundErrorMessage = "404 Not Found!";

        //Pagination
        public const int PageOffset = 10;
        public const int MaxPageCountToShow = 10;

        public class ValidationError
        {
            public const string MaxLength = "{0} max length must be {1}.";
            public const string StringLength = "The {0} must be at least {2} and at max {1} characters long.";
            public const string ConfirmPassword = "The password and confirmation password do not match.";
        }
    }
}
