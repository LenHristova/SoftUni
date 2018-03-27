using System.Collections.Generic;

using P01_Logger.Models.Appenders.Contracts;
using P01_Logger.Models.Errors.Contracts;

namespace P01_Logger.Models.Loggers.Contracts
{
    public interface ILogger
    {
        IReadOnlyCollection<IAppender> Appenders { get; }

        void Log(IError error);
    }
}
