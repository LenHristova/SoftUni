namespace ProductShop.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        public int Id { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string LastName { get; set; }

        public int? Age { get; set; }

        public ICollection<Product> BuyedProducts { get; set; } = new List<Product>();

        public ICollection<Product> SoldProducts { get; set; } = new List<Product>();
    }
}
