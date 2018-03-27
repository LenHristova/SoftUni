using System;

using P01_Logger.Models.Layouts.Contracts;

namespace P01_Logger.Models.Appenders
{
    public class ConsoleAppender : Appender
    {
        public ConsoleAppender(ILayout layout, ReportLevel reportLevel) 
            : base(layout, reportLevel)
        {
        }

        protected override void Write(string line)
        {
            Console.WriteLine(line);
        }
    }
}
