using P01_Logger.Models.Appenders.Contracts;
using P01_Logger.Models.Errors.Contracts;
using P01_Logger.Models.Layouts.Contracts;

namespace P01_Logger.Models.Appenders
{
    public abstract class Appender : IAppender
    {
        protected Appender(ILayout layout, ReportLevel reportLevel)
        {
            this.Layout = layout;
            this.ReportLevel = reportLevel;
            this.MessageCount = 0;
        }

        public ILayout Layout { get; }

        public ReportLevel ReportLevel { get; }

        public int MessageCount { get; private set; }

        public void AppendLine(IError error)
        {
            var line = this.Layout.FormatedMessage(error);
            this.Write(line);
            this.MessageCount++;
        }

        protected abstract void Write(string line);

        public override string ToString()
        {
            return $"Appender type: {this.GetType().Name}, Layout type: {this.Layout.GetType().Name}, Report level: {this.ReportLevel.ToString()}, Messages appended: {this.MessageCount}";
        }
    }
}
