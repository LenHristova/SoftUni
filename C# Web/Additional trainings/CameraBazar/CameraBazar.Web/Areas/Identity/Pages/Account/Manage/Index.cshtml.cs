namespace CameraBazar.Web.Areas.Identity.Pages.Account.Manage
{
    using CameraBazar.Data.Models;
    using Infrastructure;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Services.Contracts;
    using Services.Models.Camera;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    public class IndexModel : PageModel
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        public readonly ICameraService cameras;

        public IndexModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ICameraService cameras)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.cameras = cameras;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [RegularExpression("^\\+[0-9]{10,12}$",
                ErrorMessage = Constants.ValidationErrors.Phone)]
            public string Phone { get; set; }

            public IEnumerable<CameraListingModel> Cameras { get; set; }

            public int InStockCameras => this.Cameras.Count(c => c.Quantity > 0);

            public int OutOfStockCameras => this.Cameras.Count(c => c.Quantity <= 0);
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await userManager.GetUserAsync(User);
            var userId = this.userManager.GetUserId(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            var userName = await userManager.GetUserNameAsync(user);
            var email = await userManager.GetEmailAsync(user);
            var phoneNumber = await userManager.GetPhoneNumberAsync(user);
            var userCameras = this.cameras.ByUserId(userId);

            Username = userName;

            Input = new InputModel
            {
                Email = email,
                Phone = phoneNumber,
                Cameras = userCameras
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userManager.GetUserId(User)}'.");
            }

            var email = await userManager.GetEmailAsync(user);
            if (Input.Email != email)
            {
                var setEmailResult = await userManager.SetEmailAsync(user, Input.Email);
                if (!setEmailResult.Succeeded)
                {
                    var userId = await userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting email for user with ID '{userId}'.");
                }
            }

            var phoneNumber = await userManager.GetPhoneNumberAsync(user);
            if (Input.Phone != phoneNumber)
            {
                var setPhoneResult = await userManager.SetPhoneNumberAsync(user, Input.Phone);
                if (!setPhoneResult.Succeeded)
                {
                    var userId = await userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting phone number for user with ID '{userId}'.");
                }
            }

            await signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
