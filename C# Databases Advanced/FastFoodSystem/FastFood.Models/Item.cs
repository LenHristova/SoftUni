namespace FastFood.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Item
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Name { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    }
}
