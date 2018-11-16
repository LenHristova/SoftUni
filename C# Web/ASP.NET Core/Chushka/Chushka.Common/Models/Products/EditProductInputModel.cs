namespace Chushka.Common.Models.Products
{
    using System.ComponentModel.DataAnnotations;
    using Data.Models.Enums;

    public class EditProductInputModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [StringLength(100,
            ErrorMessage = Constants.ValidationConstants.StringLength,
            MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal Price { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public ProductType Type { get; set; }
    }
}
