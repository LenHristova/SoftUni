using System;
using System.Globalization;
using System.Linq;
using System.Reflection;

using P01_Logger.Models.Errors.Contracts;

namespace P01_Logger.Models.Errors.Factories
{
    public class ErrorFactory
    {
        private const string DATE_TIME_FORMAT = "M/d/yyyy h:mm:ss tt";

        public IError CreateError(string type, string reportLevel, string dateTimeString, string message)
        {
            var level = Validator.ValidateEnum(nameof(ReportLevel), reportLevel);
            var dateTime = Validator.ValidateDateTime(dateTimeString, DATE_TIME_FORMAT);

            var errorType = Assembly.GetExecutingAssembly()
                .GetTypes()
                .FirstOrDefault(t => t.Name == type);

            Validator.ValidateNotNullType(errorType, nameof(IError).Replace("I", ""), type);

            var error = (IError)Activator.CreateInstance(errorType, level, dateTime, message);
            return error;
        }
    }
}
