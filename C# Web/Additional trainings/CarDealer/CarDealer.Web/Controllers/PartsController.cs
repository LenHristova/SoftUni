namespace CarDealer.Web.Controllers
{
    using Infrastructure;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models;
    using Models.Parts;
    using Services.Contracts;
    using System.Collections.Generic;
    using System.Linq;
    using Services.Models.Parts;

    public class PartsController : Controller
    {
        private readonly IPartService parts;
        private readonly ISupplierService suppliers;

        public PartsController(IPartService parts, ISupplierService suppliers)
        {
            this.parts = parts;
            this.suppliers = suppliers;
        }

        public IActionResult All(int page = 1)
        {
            page = page >= 1 ? page : 1;

            var model = new PartPageListModel
            {
                Parts = this.parts.AllOnPage(page, Constants.PageOffset),
                Pagination = new Pagination(page, this.parts.Count())
            };

            if (page > model.Pagination.TotalPages)
            {
                return this.NotFoundView();
            }

            return View(model);
        }

        [Route("/parts/add")]
        public IActionResult Create()
            => base.View(new PartFormModel { Suppliers = this.GetSupplierSelectListItem() });

        [HttpPost("/parts/add")]
        public IActionResult Create(PartFormModel model)
        {
            var supplierExists = this.suppliers.Exists(model.SupplierId);

            if (!supplierExists)
            {
                ModelState.AddModelError(nameof(PartFormModel.SupplierId), "Invalid supplier!");
            }

            if (!ModelState.IsValid)
            {
                model.Suppliers = this.GetSupplierSelectListItem();
                return View(model);
            }

            this.parts.Create(
                model.Name,
                model.Quantity,
                model.Price,
                model.SupplierId);

            return RedirectToAction(nameof(All));
        }

        public IActionResult Edit(int? id)
            => this.ViewOrNotFoundView(this.parts.ById(id));

        [HttpPost]
        public IActionResult Edit(int id, PartFullInfoModel model)
        {
            var exists = this.parts.Exists(id);
            if (!exists)
            {
                return this.NotFoundView();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            this.parts.Edit(
                id,
                model.Price,
                model.Quantity);

            return RedirectToAction(nameof(All));
        }

        public IActionResult Delete(int? id)
            => this.ViewOrNotFoundView(this.parts.ById(id));

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var exists = this.parts.Exists(id);
            if (!exists)
            {
                return this.NotFoundView();
            }

            this.parts.Delete(id);

            return RedirectToAction(nameof(All));
        }

        public IActionResult Details(int? id)
            => this.ViewOrNotFoundView(this.parts.ById(id));

        private IEnumerable<SelectListItem> GetSupplierSelectListItem()
            => this.suppliers
                .All()
                .Select(s => new SelectListItem
                {
                    Text = s.Name,
                    Value = s.Id.ToString()
                });
    }
}
