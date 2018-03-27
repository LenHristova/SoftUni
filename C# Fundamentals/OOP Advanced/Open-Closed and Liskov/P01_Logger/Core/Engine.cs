using System;
using System.Collections.Generic;

using P01_Logger.Core.IO.Contracts;
using P01_Logger.Models.Appenders.Contracts;
using P01_Logger.Models.Appenders.Factories;
using P01_Logger.Models.Errors.Factories;
using P01_Logger.Models.Loggers;
using P01_Logger.Models.Loggers.Contracts;

namespace P01_Logger.Core
{
    public class Engine
    {
        private const string END_COMMAND = "END";

        private bool isRunning;
        private readonly IReader reader;
        private readonly IWriter writer;
        private readonly ILogger logger;
        private readonly ErrorFactory errorFactory;

        public Engine(IReader reader, IWriter writer, int appendersCount)
        {
            this.reader = reader;
            this.writer = writer;
            this.logger = this.InicializeLoger(appendersCount);
            this.errorFactory = new ErrorFactory();
        }

        public void Run()
        {
            this.isRunning = true;

            while (this.isRunning)
            {
                try
                {
                    this.writer.WriteGreenLine("Listening for error...");
                    this.WriteLog(this.reader.ReadLine());
                }
                catch (ArgumentException e)
                {
                    this.writer.DisplayException(e.Message);
                }
            }

            this.writer.WriteLine(logger.ToString());
        }

        //Listening for error until end command
        private void WriteLog(string input)
        {
            if (input == END_COMMAND)
            {
                this.isRunning = false;
                return;
            }

            //Get error info and log to logger by appenders' ReportLevels
            var logArgs = input.Split("|");
            var reportLevel = logArgs[0];
            var dateTime = logArgs[1];
            var message = logArgs[2];
            var errorType = "Error";
            var error = this.errorFactory.CreateError(errorType, reportLevel, dateTime, message);

            this.logger.Log(error);
        }

        private Logger InicializeLoger(int appendersCount)
        {

            var appenders = new List<IAppender>();
            var appenderFactory = new AppenderFactory();

            //Get all appenders for logger
            for (int i = 1; i <= appendersCount; i++)
            {
                try
                {
                    this.writer.WriteGreenLine($"Insert arguments for appender №{i}:");
                    var appenderArgs = this.reader.ReadLine().Split();
                    var appenderType = appenderArgs[0];
                    var layoutType = appenderArgs[1];
                    var reportLevel = appenderArgs.Length > 2 ? appenderArgs[2] : "INFO";

                    var appender = appenderFactory.CreateAppender(appenderType, layoutType, reportLevel);

                    appenders.Add(appender);
                }
                catch (ArgumentException argumentException)
                {
                    this.writer.DisplayException(argumentException.Message);
                    i--;
                }
                catch (Exception)
                {
                    this.writer.DisplayException("Ivalid input!");
                    i--;
                }
            }

            return new Logger(appenders);
        }
    }
}
