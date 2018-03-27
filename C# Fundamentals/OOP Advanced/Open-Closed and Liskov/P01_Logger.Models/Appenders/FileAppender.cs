using P01_Logger.Models.Layouts.Contracts;
using P01_Logger.Models.LogFiles.Contracts;

namespace P01_Logger.Models.Appenders
{
    public class FileAppender : Appender
    {
        public FileAppender(ILayout layout, ReportLevel reportLevel, ILogFile logFile) 
            : base(layout, reportLevel)
        {
            this.LogFile = logFile;
        }

        public ILogFile LogFile { get; }

        protected override void Write(string line)
        {
            this.LogFile.WriteAllText(line);
        }

        public override string ToString()
        {
            return base.ToString() + $", File size: {this.LogFile.Size}";
        }
    }
}
