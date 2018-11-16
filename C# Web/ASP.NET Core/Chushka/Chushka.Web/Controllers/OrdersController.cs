namespace Chushka.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Services.Contracts;
    using System.Linq;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Common.Models.Orders;
    using Data.Models;
    using Microsoft.AspNetCore.Identity;

    public class OrdersController : BaseController
    {
        private readonly IOrderService orderService;
        private readonly UserManager<User> userManager;

        public OrdersController(IOrderService orderService, UserManager<User> userManager, IMapper mapper)
            : base(mapper)
        {
            this.orderService = orderService;
            this.userManager = userManager;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult All()
        {
            var orders = this.orderService.All()
                .ProjectTo<AllOrdersViewModel>(mapper.ConfigurationProvider)
                .ToList();

            return View(orders);
        }

        [Authorize(Roles = "User, Admin")]
        [HttpPost]
        public IActionResult Create(string productId)
        {
            var userId = userManager.GetUserId(this.User);

            this.orderService.Create(productId, userId);

            return this.RedirectToAction("Index", "Home");
        }
    }
}
