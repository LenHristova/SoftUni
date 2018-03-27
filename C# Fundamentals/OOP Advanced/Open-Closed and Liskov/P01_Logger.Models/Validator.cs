using System;
using System.Globalization;

namespace P01_Logger.Models
{
    public static class Validator
    {
        public static ReportLevel ValidateEnum(string reportLevelName, string reportLevel)
        {
            if (!Enum.TryParse<ReportLevel>(reportLevel, out var level))
            {
                throw new ArgumentException($"Invalid {nameof(ReportLevel)}: \"{reportLevel}\"!");
            }

            return level;
        }

        public static void ValidateNotNullType(Type type, string expectedType, string actualType)
        {
            if (type == null)
            {
                throw new ArgumentException($"Invalid {expectedType} type: \"{actualType}\"!");
            }
        }

        public static DateTime ValidateDateTime(string dateTimeString, string dateTimeFormat)
        {
            if (!DateTime.TryParseExact(dateTimeString, dateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var dateTime))
            {
                throw new ArgumentException($"Invalid {nameof(DateTime)} format: \"{dateTimeString}\"!");
            }

            return dateTime;
        }
    }
}
