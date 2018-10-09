namespace IRunes.Web.Controllers
{
    using SIS.HTTP.Requests.Contracts;
    using SIS.HTTP.Responses.Contracts;

    public class HomeController : Controller
    {
        public HomeController(IHttpRequest httpRequest)
            : base(httpRequest) { }

        public IHttpResponse Index()
        {
            this.ViewBag["user"] = this.User;

            return this.View();
        }
    }
}
