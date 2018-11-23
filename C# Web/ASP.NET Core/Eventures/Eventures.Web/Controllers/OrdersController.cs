namespace Eventures.Web.Controllers
{
    using System.Linq;
    using Common.Models.Events;
    using Common.Models.Orders;
    using Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Services.Contracts;

    public class OrdersController : Controller
    {
        private readonly IOrderService orderService;
        private readonly UserManager<User> userManager;
        private readonly IEventService eventService;

        public OrdersController(IOrderService orderService, 
            UserManager<User> userManager, 
            IEventService eventService)
        {
            this.orderService = orderService;
            this.userManager = userManager;
            this.eventService = eventService;
        }

        [Authorize]
        public IActionResult Index()
        {
            var userId = this.userManager.GetUserId(this.User);

            var events = this.orderService.AllByUser<UserOrderListViewModel>(userId).ToList();

            return View(events);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult All()
        {
            return View(this.orderService.All<AllOrderListViewModel>().ToList());
        }

        [Authorize]
        [HttpPost]
        public IActionResult Finalize(int id, int orderedTickets)
        {
            var eventExists = this.eventService.Exists(id);

            if (!eventExists)
            {
                return RedirectToAction("All", "Events");
            }

            if (orderedTickets < 0)
            {
                this.TempData["Error"] = "Ordered tickets must be positive number.";
                return RedirectToAction("All", "Events");
            }

            var availableTickets = this.eventService.GetTicketsCount(id);

            if (availableTickets < orderedTickets)
            {
                this.TempData["Error"] = $"Not enough tickets for that event. Available tickets: {availableTickets}";
                return RedirectToAction("All", "Events");
            }

            var userId = this.userManager.GetUserId(this.User);

            this.orderService.Create(id, userId, orderedTickets);

            return RedirectToAction("Index");
        }

        //[Authorize]
        //[HttpPost]
        //public IActionResult Finalize(EventListWrapperViewModel model)
        //var @event = model.EventListViewModels.SingleOrDefault(e => e.Tickets > 0);
        //if (@event == null)
        //{
        //    ModelState.AddModelError(string.Empty, "Ordered tickets must be positive number.");
        //    //this.TempData["Error"] = "Ordered tickets must be positive number.";
        //    //return RedirectToAction("All", "Events");
        //}

        //var eventExists = this.eventService.Exists(@event.Id);

        //if (!eventExists)
        //{
        //    return RedirectToAction("All", "Events");
        //}

        //var tickets = this.eventService.GetTicketsCount(@event.Id);

        //if (tickets < @event.Tickets)
        //{
        //    this.TempData["Error"] = "Not enough tickets for this event.";
        //    return RedirectToAction("All", "Events");
        //}

        //if (!ModelState.IsValid)
        //{

        //}
        //var userId = this.userManager.GetUserId(this.User);

        //this.orderService.Create(@event.Id, userId, @event.Tickets);

        //    return RedirectToAction("Index");
        //}
    }
}
