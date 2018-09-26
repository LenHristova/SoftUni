namespace CarDealer.Web.Controllers
{
    using Infrastructure;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : Controller
    {
        public IActionResult Index() => View();

        public IActionResult PageNotFound() => this.NotFoundView();
    }
}
