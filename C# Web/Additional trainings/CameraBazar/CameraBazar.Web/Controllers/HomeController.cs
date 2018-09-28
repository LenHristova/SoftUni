namespace CameraBazar.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Models.Camera;
    using Models.Enums;
    using Services.Contracts;
    using System.Diagnostics;
    using System.Linq;
    using Services.Models.Camera;

    public class HomeController : Controller
    {
        public readonly ICameraService cameras;

        public HomeController(ICameraService cameras)
        {
            this.cameras = cameras;
        }

        //TODO Repair this
        public IActionResult Index()
            => View(this.cameras
                .All()
                .Select(c => new CameraListingModel
                {
                    Id = c.Id,
                    Make = c.Make,
                    Model = c.Model,
                    Price = c.Price,
                    ImageUrl = c.ImageUrl
                })
                .ToArray());

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
