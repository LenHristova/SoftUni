using System;
using System.IO;
using System.Linq;

using P01_Logger.Models.LogFiles.Contracts;

namespace P01_Logger.Models.LogFiles
{
    public class LogFile : ILogFile
    {
        private const string LOG_FILE_DIRECTORY = "../Resources";

        private readonly string logFilePath;

        public LogFile(string fileName)
        {
            this.logFilePath = LOG_FILE_DIRECTORY + "/" + fileName;
            this.InicializeLogFile();
        }

        public int Size { get; private set; }

        private void InicializeLogFile()
        {
            this.EnsureLogFileDirectoryExist();
            this.EnsureLogFileExist();
        }

        private void EnsureLogFileExist()
        {
            if (!File.Exists(this.logFilePath))
            {
                File.Create(this.logFilePath).Close();
            }
        }

        private void EnsureLogFileDirectoryExist()
        {
            if (!Directory.Exists(LOG_FILE_DIRECTORY))
            {
                Directory.CreateDirectory(LOG_FILE_DIRECTORY);
            }
        }

        public void WriteAllText(string line)
        {
            this.Size += line.Sum(ch => char.IsLetter(ch) ? ch : 0);
            File.AppendAllText(this.logFilePath, line + Environment.NewLine);
        }
    }
}