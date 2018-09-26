﻿namespace CarDealer.Web.Models.Sales
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class SaleFormModel
    {
        [Display(Name = "Customer")]
        public int CustomerId { get; set; }
        public IEnumerable<SelectListItem> Customers { get; set; }

        [Display(Name = "Car")]
        public int CarId { get; set; }
        public IEnumerable<SelectListItem> Cars { get; set; }

        [Range(0, 100)]
        public int Discount { get; set; }
        public IEnumerable<SelectListItem> Discounts { get; set; }
    }
}
