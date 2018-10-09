namespace IRunes.Web.ViewModels.Account
{
    using System.ComponentModel.DataAnnotations;

    public class LoginUserViewModel
    {
        [Required]
        public string UsernameOrEmail { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
