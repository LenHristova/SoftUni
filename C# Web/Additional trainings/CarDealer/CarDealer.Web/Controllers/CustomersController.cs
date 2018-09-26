namespace CarDealer.Web.Controllers
{
    using Infrastructure;
    using Microsoft.AspNetCore.Mvc;
    using Models.Customers;
    using Services.Contracts;
    using Services.Models.Enums;
    using System;
    using System.Linq;

    [Route("customers")]
    public class CustomersController : Controller
    {
        private readonly ICustomerService customers;

        public CustomersController(ICustomerService customers)
        {
            this.customers = customers;
        }

        [Route("all/{order}")]
        public IActionResult All(string order)
        {
            var isValidOrderType = Enum.TryParse<OrderType>(order, true, out var orderType);

            if (!isValidOrderType)
            {
                return this.NotFoundView();
            }

            var customerModels = this.customers.OrderedByBirthDate(orderType);

            return View(new OrderedCustomersModel
            {
                Customers = customerModels,
                OrderType = orderType.ToString()
            });
        }

        [Route("{id?}")]
        public IActionResult Details(int? id)
        {
            var customer = this.customers.ById(id);

            if (customer == null)
            {
                return this.NotFoundView();
            }

            return View(new CustomerSalesModel
            {
                Name = customer.Name,
                BoughtCars = customer.BoughtCars,
                TotalSpentMoney = customer.SaleInfo.Sum(si => si.CarPrice * (1 - (decimal)si.Discount))
            });
        }
    }
}
