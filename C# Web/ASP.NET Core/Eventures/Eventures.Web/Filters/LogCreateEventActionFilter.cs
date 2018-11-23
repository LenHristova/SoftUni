namespace Eventures.Web.Filters
{
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.Logging;
    using Services.Contracts;
    using System;
    using Logging;

    public class LogCreateEventActionFilter : ActionFilterAttribute
    {
        private readonly ILogger logger;
        private readonly IEventService eventService;

        public LogCreateEventActionFilter(
            ILogger<LogCreateEventActionFilter> logger, 
            IEventService eventService)
        {
            this.logger = logger;
            this.eventService = eventService;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.ModelState.IsValid)
            {
                var dateTime = DateTime.UtcNow;
                var username = context.HttpContext.User.Identity.Name;
                var eventToLog = this.eventService.GetLastAdded();

                var logMessage =
                    $"[{dateTime}] Administrator {username} create event {eventToLog?.Name} " +
                    $"({eventToLog?.Start} / {eventToLog?.End}";

                this.logger.LogInformation(LoggingEvents.InsertItem, logMessage);

                base.OnActionExecuted(context);
            }
        }
    }
}
