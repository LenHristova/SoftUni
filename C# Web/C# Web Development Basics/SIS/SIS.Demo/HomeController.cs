namespace SIS.Demo
{
    using HTTP.Enums;
    using HTTP.Responses.Contracts;
    using WebServer.Results;

    public class HomeController
    {
        public IHttpResponse Index()
        {
            //var content = "<h1>Hello, world!</h1>";
            var content = "<form method=\"post\">" +
                          "<div><label for=\"name\">Name</label>" +
                          "<input name=\"name\" type=\"text\"/>" +
                          "<label for=\"price\">Price</label>" +
                          "<input name=\"price\" type=\"text\"/>" +
                          "<input type=\"submit\" value=\"Add\" /></div>" +
                          "</form>";

            return new HtmlResult(content, HttpResponseStatusCode.Ok);
        }
    }
}
