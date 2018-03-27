using System.Collections.Generic;
using System.Text;

using P01_Logger.Models.Appenders.Contracts;
using P01_Logger.Models.Errors.Contracts;
using P01_Logger.Models.Loggers.Contracts;

namespace P01_Logger.Models.Loggers
{
    public class Logger : ILogger
    {
        private readonly ICollection<IAppender> appenders;

        public Logger(ICollection<IAppender> appenders)
        {
            this.appenders = appenders;
        }

        public IReadOnlyCollection<IAppender> Appenders => (IReadOnlyCollection<IAppender>) this.appenders;

        public void Log(IError error)
        {
            foreach (var appender in this.Appenders)
            {
                if (appender.ReportLevel <= error.ReportLevel)
                {
                    appender.AppendLine(error);
                }
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine("Logger info");
            foreach (var appender in this.Appenders)
            {
                sb.AppendLine(appender.ToString());
            }

            return sb.ToString().TrimEnd();
        }
    }
}
