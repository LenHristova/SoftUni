using System.Globalization;

using P01_Logger.Models.Errors.Contracts;
using P01_Logger.Models.Layouts.Contracts;

namespace P01_Logger.Models.Layouts
{
    public class SimpleLayout : ILayout
    {        
        //"<date-time> - <report level> - <message>"
        private const string FORMAT = "{0} - {1} - {2}";
        private const string DATE_FORMAT = "M/d/yyyy h:mm:ss tt";

        public SimpleLayout() 
        {
        }

        public string FormatedMessage(IError error)
        {
            var result = string.Format(
                FORMAT,
                error.DateTime.ToString(DATE_FORMAT, CultureInfo.InvariantCulture),
                error.ReportLevel,
                error.Message);
            return result;
        }
    }
}
