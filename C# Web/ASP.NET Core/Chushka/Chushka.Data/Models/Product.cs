namespace Chushka.Data.Models
{
    using System.Collections.Generic;
    using Enums;

    public class Product
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public ProductType Type { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<Order> Orders { get; set; } = new HashSet<Order>();
    }
}
