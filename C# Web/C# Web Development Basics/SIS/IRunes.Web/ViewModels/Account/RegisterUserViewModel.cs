namespace IRunes.Web.ViewModels.Account
{
    using System.ComponentModel.DataAnnotations;
    using Common;

    public class RegisterUserViewModel
    {
        [Required]
        [StringLength(20, MinimumLength = 3,
            ErrorMessage = Constants.ValidationErrorMessages.StringLength)]
        [RegularExpression("[A-Za-z0-9]+",
            ErrorMessage = Constants.ValidationErrorMessages.LettersAndDigitsOnly)]
        public string Username { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 6,
            ErrorMessage = Constants.ValidationErrorMessages.StringLength)]
        public string Password { get; set; }

        [Display(Name = "Confirmed password")]
        [Required]
        public string ConfirmPassword { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(20, MinimumLength = 3,
            ErrorMessage = Constants.ValidationErrorMessages.StringLength)]
        public string Email { get; set; }
    }
}
