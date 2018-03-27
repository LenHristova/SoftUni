using P01_Logger.Models.LogFiles.Contracts;

namespace P01_Logger.Models.LogFiles.Factories
{
    public class LogFileFactory
    {
        private const string LOG_FILE_NAME = "log{0}.txt";
        private int logFilesCount;

        public LogFileFactory()
        {
            this.logFilesCount = 0;
        }

        public ILogFile CreateLogFile()
        {
            var logFileName = string.Format(LOG_FILE_NAME, this.logFilesCount);
            var logFile = new LogFile(logFileName);
            this.logFilesCount++;
            return logFile;
        }
    }
}
