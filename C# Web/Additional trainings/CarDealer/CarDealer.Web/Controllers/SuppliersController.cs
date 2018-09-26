namespace CarDealer.Web.Controllers
{
    using Infrastructure;
    using Microsoft.AspNetCore.Mvc;
    using Models.Suppliers;
    using Services.Contracts;
    using Services.Models.Enums;
    using System;

    [Route("suppliers")]
    public class SuppliersController : Controller
    {
        private readonly ISupplierService suppliers;

        public SuppliersController(ISupplierService suppliers)
        {
            this.suppliers = suppliers;
        }

        [Route("all/{type}")]
        public IActionResult All(string type)
        {
            var isValidOrderType = Enum.TryParse<SupplierType>(type, true, out var supplierType);

            if (!isValidOrderType)
            {
                return this.NotFoundView();
            }

            var isImporter = supplierType == SupplierType.Importer;
            var supplierModels = this.suppliers.All(isImporter: isImporter);

            return View(new SuppliersByIsImporterOrLocal
            {
                Type = supplierType,
                Suppliers = supplierModels
            });
        }

        [Route("{id?}")]
        public IActionResult Details(int? id)
            => this.ViewOrNotFoundView(this.suppliers.ById(id));
    }
}
