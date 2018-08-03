namespace ProductShop.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Product
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 3)]
        public string Name { get; set; }

        public decimal Price { get; set; }

        public int? BuyerId { get; set; }
        public User Buyer { get; set; }

        public int SellerId { get; set; }
        public User Seller { get; set; }

        public ICollection<CategoryProduct> Categories { get; set; } = new List<CategoryProduct>();
    }
}
