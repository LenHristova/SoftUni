namespace Eventures.Web.Controllers
{
    using Common.Models.Events;
    using Filters;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Services.Contracts;
    using System.Linq;

    public class EventsController : Controller
    {
        private readonly IEventService eventService;
        private readonly ILogger logger;

        public EventsController(
            IEventService eventService,
            ILogger<EventsController> logger)
        {
            this.eventService = eventService;
            this.logger = logger;
        }

        [Authorize]
        public IActionResult All()
        {
            var events = this.eventService.All<EventListViewModel>().ToList();

            return View(new EventListWrapperViewModel { EventListViewModels = events });
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create() => View();

        [TypeFilter(typeof(LogCreateEventActionFilter))]
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Create(CreateEventInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var eventDb = this.eventService.Create(model);

            //LogCreateEventActionFilter logs message now.
            // this.logger.LogInformation(LoggingEvents.InsertItem, $"Event created: {eventDb.Name}", eventDb);

            return RedirectToAction(nameof(All));
        }
    }
}
