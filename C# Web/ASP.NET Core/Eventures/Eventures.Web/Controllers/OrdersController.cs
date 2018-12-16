namespace Eventures.Web.Controllers
{
    using Common.Models.Orders;
    using Eventures.Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Services.Contracts;
    using System.Linq;
    using Common.Constants;

    public class OrdersController : Controller
    {
        private readonly IOrderService orderService;
        private readonly UserManager<User> userManager;
        private readonly IEventService eventService;
        private readonly IPaginationService paginationService;

        public OrdersController(
            IOrderService orderService,
            UserManager<User> userManager,
            IEventService eventService,
            IPaginationService paginationService)
        {
            this.orderService = orderService;
            this.userManager = userManager;
            this.eventService = eventService;
            this.paginationService = paginationService;
        }

        [Authorize]
        public IActionResult Index(int? page)
        {
            var userId = this.userManager.GetUserId(this.User);

            var orderedEvents = this.orderService.AllByUser<UserOrderListViewModel>(userId).ToList();

            var pageNumber = this.paginationService.ValidatePage(page, orderedEvents.Count);
            var eventsInPage = this.paginationService.Paginate(pageNumber, orderedEvents);

            var firstEventNumberOnPage = (pageNumber - 1) * GlobalConstants.OrdersCountOnPage + 1;

            for (int i = 0; i < eventsInPage.Count; i++)
            {
                eventsInPage[i].Number = firstEventNumberOnPage + i;
            }

            return View(new UserOrderListOnPageViewModel { Events = eventsInPage });
        }

        [Authorize(Roles = "Admin")]
        public IActionResult All(int? page)
        {
            var orders = this.orderService.All<AllOrderListViewModel>().ToList();

            var pageNumber = this.paginationService.ValidatePage(page, orders.Count);
            var ordersInPage = this.paginationService.Paginate(pageNumber, orders);

            var firstOrderNumberOnPage = (pageNumber - 1) * GlobalConstants.OrdersCountOnPage + 1;

            for (int i = 0; i < ordersInPage.Count; i++)
            {
                ordersInPage[i].Number = firstOrderNumberOnPage + i;
            }

            return View(new AllOrderListOnPageViewModel { Orders = ordersInPage });
        }

        [Authorize]
        [HttpPost]
        public IActionResult Finalize(int id, int orderedTickets)
        {
            var eventExists = this.eventService.IsAvailable(id);

            if (!eventExists)
            {
                this.TempData["Error"] = "The event that you're trying to order tickets for, doesn't exist or orders are not available.";
                return RedirectToAction("Index", "Events");
            }

            if (orderedTickets <= 0)
            {
                this.TempData["Error"] = "Ordered tickets must be positive number.";
                return RedirectToAction("Index", "Events");
            }

            var availableTickets = this.eventService.GetTicketsCount(id);

            if (!availableTickets.HasValue || availableTickets < orderedTickets)
            {
                this.TempData["Error"] = $"Not enough tickets for that event. Available tickets: {availableTickets}";
                return RedirectToAction("Index", "Events");
            }

            var userId = this.userManager.GetUserId(this.User);
            var success = this.eventService.BuyTickets(id, orderedTickets);
            if (success)
            {
                this.orderService.Create(id, userId, orderedTickets);
            }

            return RedirectToAction("Index");
        }
    }
}
