namespace Eventures.Web.Logging
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Logging;
    using System;

    public static class DbLoggerExtensions
    {
        public static ILoggerFactory AddContext(
            this ILoggerFactory factory,
            IApplicationBuilder app, 
            Func<string, LogLevel, 
                bool> filter = null)
        {
            factory.AddProvider(new DbLoggerProvider(filter, app));
            return factory;
        }

        public static ILoggerFactory AddContext(
            this ILoggerFactory factory, 
            IApplicationBuilder app, 
            LogLevel minLevel)
            => AddContext(factory, app, (_, logLevel) => logLevel >= minLevel);
    }
}
