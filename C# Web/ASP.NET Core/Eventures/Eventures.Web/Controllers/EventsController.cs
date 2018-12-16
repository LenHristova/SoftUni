namespace Eventures.Web.Controllers
{
    using Common.Models.Events;
    using Filters;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Services.Contracts;
    using System.Linq;
    using Common.Constants;

    public class EventsController : Controller
    {
        private readonly IEventService eventService;
        private readonly IPaginationService paginationService;
        private readonly ILogger logger;

        public EventsController(
            IEventService eventService,
            IPaginationService paginationService,
            ILogger<EventsController> logger)
        {
            this.eventService = eventService;
            this.paginationService = paginationService;
            this.logger = logger;
        }

        [Authorize]
        public IActionResult Index(int? page)
        {
            var events = this.eventService.All<EventListViewModel>().ToList();
            var pageNumber = this.paginationService.ValidatePage(page, events.Count);
            var eventsInPage = this.paginationService.Paginate(pageNumber, events);

            var firstEventNumberOnPage = (pageNumber - 1) * GlobalConstants.EventsCountOnPage + 1;

            for (int i = 0; i < eventsInPage.Count; i++)
            {
                eventsInPage[i].Number = firstEventNumberOnPage + i;
            }

            return View(new EventListOnPageViewModel { Events = eventsInPage });
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

            return RedirectToAction(nameof(Index));
        }
    }
}
