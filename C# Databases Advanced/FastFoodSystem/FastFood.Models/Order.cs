namespace FastFood.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Enums;

    public class Order
    {
        public int Id { get; set; }

        [Required]
        public string Customer { get; set; }

        public DateTime DateTime { get; set; }

        public OrderType Type { get; set; }

        [NotMapped]
        public decimal TotalPrice => this.OrderItems.Sum(oi => oi.Quantity * oi.Item.Price);

        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    }
}
