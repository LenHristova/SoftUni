namespace CameraBazar.Web.Areas.Identity.Pages.Account
{
    using CameraBazar.Data.Models;
    using Infrastructure;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Logging;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;

    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;
        private readonly ILogger<RegisterModel> logger;

        public RegisterModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<RegisterModel> logger)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            [StringLength(20,
                ErrorMessage = Constants.ValidationErrors.StringLength,
                MinimumLength = 4)]
            [RegularExpression("^[A-Za-z]+$",
                ErrorMessage = Constants.ValidationErrors.LettersOnly)]
            public string Username { get; set; }

            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [StringLength(100,
                ErrorMessage = Constants.ValidationErrors.StringLength,
                MinimumLength = 3)]
            [RegularExpression("^[a-z0-9]+$",
                ErrorMessage = Constants.ValidationErrors.LowercaseLettersAndDigitsOnly)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password",
                ErrorMessage = Constants.ValidationErrors.PasswordConfirmation)]
            public string ConfirmPassword { get; set; }

            [Required]
            [RegularExpression("^\\+[0-9]{10,12}$",
                ErrorMessage = Constants.ValidationErrors.Phone)]
            public string Phone { get; set; }
        }

        public void OnGet(string returnUrl = null)
            => ReturnUrl = returnUrl;

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = Input.Username,
                    Email = Input.Email,
                    PhoneNumber = Input.Phone
                };

                var result = await userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    logger.LogInformation("User created a new account with password.");

                    await signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return Page();
        }
    }
}
