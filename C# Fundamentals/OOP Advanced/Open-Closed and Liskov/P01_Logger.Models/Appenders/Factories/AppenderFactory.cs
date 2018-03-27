using P01_Logger.Models.Appenders.Contracts;
using P01_Logger.Models.Layouts.Factories;
using P01_Logger.Models.LogFiles.Factories;

namespace P01_Logger.Models.Appenders.Factories
{
    public class AppenderFactory
    {      
        private readonly LayoutFactory layoutFactory;
        private readonly LogFileFactory logFileFactory;

        public AppenderFactory()
        {
            this.layoutFactory = new LayoutFactory();
            this.logFileFactory=new LogFileFactory();
        }

        public IAppender CreateAppender(string type, string layoutType, string reportLevel)
        {
            var layout = this.layoutFactory.CreateLayout(layoutType);

            var level = Validator.ValidateEnum(nameof(ReportLevel), reportLevel);

            IAppender appender = null;
            switch (type)
            {
                case nameof(ConsoleAppender):
                    appender = new ConsoleAppender(layout, level);
                    break;
                case nameof(FileAppender):
                    var logfile = this.logFileFactory.CreateLogFile();
                    appender = new FileAppender(layout, level, logfile);
                    break;
            }

            Validator.ValidateNotNullType(appender?.GetType(), nameof(IAppender).Replace("I", ""), type);

            return appender;
        }
    }
}
