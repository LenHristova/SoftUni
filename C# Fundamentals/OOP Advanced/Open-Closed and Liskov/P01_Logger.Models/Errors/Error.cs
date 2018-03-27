using System;

using P01_Logger.Models.Errors.Contracts;

namespace P01_Logger.Models.Errors
{
    public class Error : IError
    {
        public Error(ReportLevel reportLevel, DateTime dateTime, string message)
        {
            this.ReportLevel = reportLevel;
            this.Message = message;
            this.DateTime = dateTime;
        }

        public DateTime DateTime { get; }

        public ReportLevel ReportLevel { get; }

        public string Message { get; }
    }
}
