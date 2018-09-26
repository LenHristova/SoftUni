namespace CarDealer.Web.Models.Customers
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CustomerFormModel
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Birth date")]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "Is young driver")]
        public bool IsYoungDriver { get; set; }
    }
}
