namespace Eventures.Web.Logging
{
    using Eventures.Data.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Services.Contracts;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class DbLogger : ILogger
    {
        private readonly string categoryName;
        private readonly Func<string, LogLevel, bool> filter;
        private bool selfException;
        private readonly IApplicationBuilder app;

        public DbLogger(
            string categoryName,
            Func<string, LogLevel, bool> filter,
            IApplicationBuilder app)
        {
            this.categoryName = categoryName;
            this.filter = filter;
            this.app = app;
        }

        public void Log<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception exception,
            Func<TState, Exception, string> formatter)
        {
            if (!IsAllowed(eventId.Id) && !IsEnabled(logLevel))
            {
                return;
            }

            if (selfException)
            {
                selfException = false;
                return;
            }

            selfException = true;
            if (formatter == null)
            {
                throw new ArgumentNullException(nameof(formatter));
            }

            var message = formatter(state, exception);
            if (string.IsNullOrEmpty(message))
            {
                return;
            }

            if (exception != null)
            {
                message += "\n" + exception;
            }

            try
            {
                var maxMessageLength = this.GetMaxMessageLength();
                message = maxMessageLength != null && message.Length > maxMessageLength
                    ? message.Substring(0, (int)maxMessageLength)
                    : message;

                var serviceFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
                var scope = serviceFactory.CreateScope();
                using (scope)
                {
                    var logService = scope.ServiceProvider.GetRequiredService<ILogService>();
                    logService.Log(message, eventId.Id, logLevel);
                }

                selfException = false;
            }
            catch (Exception ex)
            {
                var test = ex;
            }
        }

        private static bool IsAllowed(int eventIdId)
            => eventIdId >= 1000 && eventIdId <= 1005;

        public bool IsEnabled(LogLevel logLevel)
            => filter == null || filter(categoryName, logLevel);

        public IDisposable BeginScope<TState>(TState state) => null;

        private int? GetMaxMessageLength()
        {
            int? maxLength = null;
            var props = typeof(Log).GetProperties();
            foreach (var prop in props)
            {
                var attrs = prop.GetCustomAttributes(true);
                foreach (var attr in attrs)
                {
                    if (attr is MaxLengthAttribute maxLengthAttr && prop.Name.Equals("Message"))
                    {
                        maxLength = maxLengthAttr.Length;
                    }
                }
            }

            return maxLength;
        }
    }
}