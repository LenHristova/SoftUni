using System.Globalization;

using P01_Logger.Models.Errors.Contracts;
using P01_Logger.Models.Layouts.Contracts;

namespace P01_Logger.Models.Layouts
{
    public class XmlLayout : ILayout
    {
        //<log>
        //  <date>3/31/2015 5:33:07 PM</date>
        //  <level>ERROR</level>
        //  <message>Error parsing request</message>
        //</log>
        private const string FORMAT = "<log>\r\n" +
                                      "\t<date>{0}</date>\r\n" +
                                      "\t<level>{1}</level>\r\n" +
                                      "\t<message>{2}</message>\r\n" +
                                      "</log>";
        private const string DATE_FORMAT = "M/d/yyyy h:mm:ss tt";

        public XmlLayout()
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
