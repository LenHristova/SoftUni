namespace Eventures.Web.Logging
{
    using Microsoft.Extensions.Logging;
    using System;
    using Microsoft.AspNetCore.Builder;
    using Services.Contracts;

    public class DbLoggerProvider : ILoggerProvider
    {
        private readonly Func<string, LogLevel, bool> filter;
        private readonly IApplicationBuilder app;

        public DbLoggerProvider(Func<string, LogLevel, bool> filter, IApplicationBuilder app)
        {
            this.filter = filter;
            this.app = app;
        }

        public ILogger CreateLogger(string categoryName) 
            => new DbLogger(categoryName, filter, app);

        public void Dispose() { }
    }
}
