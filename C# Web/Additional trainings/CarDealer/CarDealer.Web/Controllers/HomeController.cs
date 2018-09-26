namespace CarDealer.Web.Controllers
{
    using Infrastructure;
    using Microsoft.AspNetCore.Mvc;
    using Models;

    public class HomeController : Controller
    {
        public IActionResult Index() => View();

        public IActionResult Privacy() => View();

        public IActionResult PageNotFound() => this.NotFoundView();

        public IActionResult Error() 
            => View("Error", new ErrorModel("Oops! Something's happened..."));
    }
}
