namespace Eventures.Services.Contracts
{
    using Microsoft.Extensions.Logging;

    public interface ILogService
    {
        void Log(string message, EventId eventId, LogLevel logLevel);
    }
}
