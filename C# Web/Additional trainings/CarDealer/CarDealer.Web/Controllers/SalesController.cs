namespace CarDealer.Web.Controllers
{
    using Infrastructure;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models.Sales;
    using Services.Contracts;
    using Services.Models.Enums;
    using System;
    using System.Linq;

    [Route("sales")]
    public class SalesController : Controller
    {
        private readonly ISaleService sales;
        private readonly ICustomerService customers;
        private readonly ICarService cars;

        public SalesController(ISaleService sales, ICustomerService customers, ICarService cars)
        {
            this.customers = customers;
            this.cars = cars;
            this.sales = sales;
        }

        [Route("{type}")]
        public IActionResult All(string type)
        {
            var isValidSaleType = Enum.TryParse<SaleType>(type, true, out var saleType);

            if (!isValidSaleType)
            {
                return this.NotFoundView();
            }

            var allSales = this.sales.All(saleType);

            return View(new SalesByPredicateModel
            {
                Sales = allSales,
                SaleType = saleType.ToString()
            });
        }

        [Authorize]
        [Route("add")]
        public IActionResult Create()
            => View(GetSaleFormModel());

        [Authorize]
        [Route("create")]
        public IActionResult Create(SaleFormModel model)
        {
            var customer = this.customers.GetCustomerSaleInfoModel(model.CustomerId);
            if (customer == null)
            {
                ModelState.AddModelError(nameof(SaleFormModel.CustomerId), "Invalid customer!");
            }

            var car = this.cars.GetCarPriceModel(model.CarId);
            if (car == null)
            {
                ModelState.AddModelError(nameof(SaleFormModel.CarId), "Invalid car!");
            }

            if (!ModelState.IsValid)
            {
                return View(this.GetSaleFormModel());
            }

            var sale = new SaleFinalizeModel
            {
                Car = car,
                Customer = customer,
                Discount = model.Discount
            };

            return View(nameof(FinalizeOrder), sale);
        }

        [Authorize]
        [HttpPost("finalize")]
        public IActionResult FinalizeOrder(SaleFinalizeModel model)
        {
            var customerExists = this.customers.Exists(model.Customer.Id);
            if (!customerExists)
            {
                return this.NotFoundView();
            }

            var carExists = this.cars.Exists(model.Car.Id);
            if (!carExists || model.Discount < 0 || model.Discount > 100)
            {
                return this.NotFoundView();
            }

            var saleId = this.sales.Create(
                model.Customer.Id,
                model.Car.Id,
                model.Discount / 100.0);

            return Redirect($"/sales/details/{saleId}");
        }

        [Route("details/{id?}")]
        public IActionResult Details(int? id)
            => this.ViewOrNotFoundView(this.sales.ById(id));

        private SaleFormModel GetSaleFormModel()
            => new SaleFormModel
            {
                Customers = this.customers
                    .All()
                    .Select(c => new SelectListItem
                    {
                        Text = c.Name,
                        Value = c.Id.ToString()
                    }),
                Cars = this.cars
                    .All()
                    .Select(c => new SelectListItem
                    {
                        Text = c.Make + c.Model,
                        Value = c.Id.ToString()
                    }),
                Discounts = Enumerable.Range(0, 100)
                    .Select(i => new SelectListItem
                    {
                        Text = i.ToString() + "%",
                        Value = i.ToString()
                    })
            };
    }
}
