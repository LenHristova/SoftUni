using System.ComponentModel.DataAnnotations;

namespace BookShop.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Category
    {
        public Category()
        {
            this.CategoryBooks = new List<BookCategory>();
        }

        [Key]
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public ICollection<BookCategory> CategoryBooks { get; set; }

    }
}
