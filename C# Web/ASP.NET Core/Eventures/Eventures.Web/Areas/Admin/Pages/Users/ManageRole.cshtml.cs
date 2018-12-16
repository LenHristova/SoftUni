namespace Eventures.Web.Areas.Admin.Pages.Users
{
    using Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [Authorize(Roles = "Admin")]
    public class ManageRoleModel : PageModel
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public ManageRoleModel(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public UsersListWrapperViewModel UsersListWrapper { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class UsersListWrapperViewModel
        {
            public IList<UsersListViewModel> Users { get; set; }
        }

        public class UsersListViewModel
        {
            public string Id { get; set; }

            public string Username { get; set; }

            public string Role { get; set; }
        }

        public void OnGet()
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            var users = new List<UsersListViewModel>();
            foreach (var user in this.userManager.Users)
            {
                if (user.UserName == this.User.Identity.Name)
                {
                    continue;
                }

                var role = this.userManager.GetRolesAsync(user).GetAwaiter().GetResult();

                var userModel = new UsersListViewModel
                {
                    Id = user.Id,
                    Username = user.UserName,
                    Role = role.FirstOrDefault()
                };

                users.Add(userModel);
            }

            this.UsersListWrapper = new UsersListWrapperViewModel { Users = users };
            this.ViewData["Roles"] = this.roleManager.Roles.Select(r => r.Name);
        }

        public async Task<IActionResult> OnPost(string userId, string currentRole, string newRole)
        {
            var user = this.userManager.Users.FirstOrDefault(u => u.Id == userId);
            var currentRoleExists = await this.roleManager.RoleExistsAsync(currentRole);
            var newRoleExists = await this.roleManager.RoleExistsAsync(currentRole);

            if (user != null && currentRoleExists && newRoleExists)
            {
                await this.userManager.RemoveFromRoleAsync(user, currentRole);
                await this.userManager.AddToRoleAsync(user, newRole);
            }
            else 
            {
                this.TempData["Error"] = "Invalid data.";
            }

            return RedirectToPage("./ManageRole");
        }
    }
}