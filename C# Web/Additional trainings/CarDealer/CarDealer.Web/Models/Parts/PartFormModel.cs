namespace CarDealer.Web.Models.Parts
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class PartFormModel
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Range(1, int.MaxValue,
            ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }

        [Range(typeof(decimal), "0", "79228162514264337593543950335",
            ErrorMessage = "Price must be positive.")]
        public decimal Price { get; set; }

        [Display(Name = "Supplier")]
        public int SupplierId { get; set; }

        public IEnumerable<SelectListItem> Suppliers { get; set; }
    }
}
