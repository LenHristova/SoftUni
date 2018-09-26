namespace CarDealer.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Models.Sales;
    using Services.Contracts;
    using Services.Models.Enums;
    using System;
    using Infrastructure;

    [Route("sales")]
    public class SalesController : Controller
    {
        private readonly ISaleService sales;

        public SalesController(ISaleService sales)
        {
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

        [Route("details/{id?}")]
        public IActionResult Details(int? id)
            => this.ViewOrNotFoundView(this.sales.ById(id));
    }
}
