namespace CarDealer.Web.Controllers
{
    using Infrastructure;
    using Microsoft.AspNetCore.Mvc;
    using Models.Cars;
    using Services.Contracts;

    [Route("cars")]
    public class CarsController : Controller
    {
        private readonly ICarService cars;

        public CarsController(ICarService cars)
        {
            this.cars = cars;
        }

        [Route("all")]
        public IActionResult All() => View(this.cars.AllMakes());

        [Route("{make}")]
        public IActionResult ByMake(string make)
        => View(new OrderedCarsFromMake
        {
            Make = make,
            Cars = this.cars.ByMake(make)
        });

        [Route("{id?}/parts")]
        public IActionResult Details(int? id)
            => this.ViewOrNotFoundView(this.cars.ById(id));
    }
}
