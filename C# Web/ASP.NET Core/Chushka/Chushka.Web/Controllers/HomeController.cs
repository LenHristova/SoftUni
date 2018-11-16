namespace Chushka.Web.Controllers
{
    using System;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Common.Models.Products;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services.Contracts;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using Common.Models.Home;
    using Data.Models;
    using Microsoft.AspNetCore.Identity;

    public class HomeController : BaseController
    {
        private readonly UserManager<User> userManager;
        private readonly IProductService productService;

        public HomeController(UserManager<User> userManager, IProductService productService, IMapper mapper)
            : base(mapper)
        {
            this.userManager = userManager;
            this.productService = productService;
        }

        public IActionResult Index()
        {
            var model = new IndexViewModel();

            if (User.Identity.IsAuthenticated)
            {
                model.LoggedInUserFullName = this.userManager.GetUserAsync(User).GetAwaiter().GetResult().FullName;

                var products = this.productService.All()
                    .ProjectTo<IndexProductViewModel>(mapper.ConfigurationProvider)
                    .ToList();

                model.RowsProducts = new List<ICollection<IndexProductViewModel>>();

                var rowsCount = Math.Ceiling(products.Count / 5.0);
                var currentIndex = 0;

                for (int j = 0; j < rowsCount; j++)
                {
                    var rowProducts = new List<IndexProductViewModel>();
                    for (int i = 0; i < 5; i++)
                    {
                        if (currentIndex >= products.Count)
                        {
                            break;
                        }

                        rowProducts.Add(products[currentIndex++]);
                    }

                    model.RowsProducts.Add(rowProducts);
                }
            }

            return View(model);
        }

        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
