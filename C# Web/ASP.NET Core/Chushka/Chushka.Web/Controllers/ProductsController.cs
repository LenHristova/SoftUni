namespace Chushka.Web.Controllers
{
    using AutoMapper;
    using Common.Models.Products;
    using Data.Models.Enums;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Services.Contracts;

    public class ProductsController : BaseController
    {
        private readonly IProductService productService;

        public ProductsController(IProductService productService, IMapper mapper)
            : base(mapper)
        {
            this.productService = productService;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create() => View(new CreateProductInputModel { Type = ProductType.Other });

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Create(CreateProductInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            this.productService.Create(model);
            return this.RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(string id)
        {
            var product = this.productService.Get(id);

            if (product == null)
            {
                return this.RedirectToAction("Index", "Home");
            }

            var model = this.mapper.Map<EditProductInputModel>(product);
            return this.View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Edit(EditProductInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            this.productService.Edit(model);

            return this.RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(string id)
        {
            var product = this.productService.Get(id);

            if (product == null)
            {
                return this.RedirectToAction("Index", "Home");
            }

            var model = this.mapper.Map<DeleteProductInputModel>(product);
            return this.View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Delete(string id, string name)
        {
            this.productService.Delete(id);

            return this.RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "User, Admin")]
        public IActionResult Details(string id)
        {
            var product = this.productService.Get(id);

            if (product == null)
            {
                return this.RedirectToAction("Index", "Home");
            }

            var model = this.mapper.Map<DetailsProductViewModel>(product);
            return this.View(model);
        }
    }
}
