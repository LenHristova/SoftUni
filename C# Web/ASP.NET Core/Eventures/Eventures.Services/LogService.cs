namespace Eventures.Services
{
    using System;
    using Contracts;
    using Data;
    using Data.Models;
    using Microsoft.Extensions.Logging;

    public class LogService : ILogService
    {
        private readonly EventuresDbContext db;

        public LogService(EventuresDbContext db)
        {
            this.db = db;
        }

        public void Log(string message, EventId eventId, LogLevel logLevel)
        {
            var log = new Log
            {
                Message = message,
                EventId = eventId.Id,
                LogLevel = logLevel.ToString(),
                CreatedTime = DateTime.UtcNow
            };

            db.Logs.Add(log);

            db.SaveChanges();
        }
    }
}
