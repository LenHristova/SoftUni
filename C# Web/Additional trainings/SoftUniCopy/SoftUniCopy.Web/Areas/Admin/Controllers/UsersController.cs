namespace SoftUniCopy.Web.Areas.Admin.Controllers
{
    using System.Collections.Immutable;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models.Users;
    using Services.Contracts;
    using SoftUniCopy.Models;

    public class UsersController : AdminController
    {
        private readonly IUserService users;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;

        public UsersController(IUserService users, IMapper mapper, UserManager<User> userManager)
        {
            this.users = users;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        public IActionResult Index()
            => View(this.users.All<UserListingModel>()
                .Where(u => u.Id != this.userManager.GetUserId(this.User)));

        public async Task<IActionResult> Details(string id)
        {
            var currentUserId = this.userManager.GetUserId(this.User);
            if (currentUserId == id)
            {
                return Unauthorized();
            }

            var user = await this.users.ById<User>(id);
            if (user == null)
            {
                return NotFound();
            }

            var model = this.mapper.Map<UserDetailsModel>(user);
            model.Roles = await this.userManager.GetRolesAsync(user);

            return View(model);
        }
    }
}
