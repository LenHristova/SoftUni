namespace FastFood.DataProcessor.Dto.Import
{
    using System.ComponentModel.DataAnnotations;

    public class ItemDto
    {
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Category { get; set; }

        [Range(0.01, double.PositiveInfinity)]
        public decimal Price { get; set; }
    }
}
