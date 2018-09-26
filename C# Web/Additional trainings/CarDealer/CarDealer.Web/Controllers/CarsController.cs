namespace CarDealer.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using Infrastructure;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models.Cars;
    using Services.Contracts;
    using CarCreateModel = Models.Cars.CarCreateModel;

    [Route("cars")]
    public class CarsController : Controller
    {
        private readonly ICarService cars;
        private readonly IPartService parts;

        public CarsController(ICarService cars, IPartService parts)
        {
            this.cars = cars;
            this.parts = parts;
        }

        [Route("all")]
        public IActionResult All() => View(this.cars.AllMakes());

        [Authorize]
        [Route("add")]
        public IActionResult Create()
            => base.View(new CarCreateModel
            {
                Parts = this.GetAllParts()
            });

        [Authorize]
        [HttpPost("add")]
        public IActionResult Create(CarCreateModel carModel)
        {
            if (!ModelState.IsValid)
            {
                carModel.Parts = this.GetAllParts();
                return View(carModel);
            }

            this.cars.Create(
                carModel.Make,
                carModel.Model,
                carModel.TraveledDistance,
                carModel.SelectedPartsIds);

            return RedirectToAction(nameof(All));
        }

        [Route("{make}")]
        public IActionResult ByMake(string make)
        => View(new OrderedCarsFromMake
        {
            Make = make,
            Cars = this.cars.ByMake(make)
        });

        [Route("{id?}/parts")]
        public IActionResult CarParts(int? id)
            => this.ViewOrNotFoundView(this.cars.ById(id));

        private IEnumerable<SelectListItem> GetAllParts()
            => this.parts
                .All()
                .Select(p => new SelectListItem
                {
                    Text = $"{p.Name} (${p.Price:F2})",
                    Value = p.Id.ToString()
                });
    }
}
