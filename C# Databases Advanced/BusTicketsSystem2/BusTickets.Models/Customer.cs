namespace BusTickets.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Enums;

    public class Customer : EntityBase
    {
        [Required]
        [MaxLength(80)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(80)]
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Gender Gender { get; set; }

        public bool? IsDeleted { get; set; }

        public int? HomeTownId { get; set; }
        public virtual Town HomeTown { get; set; }

        public virtual BankAccount BankAccount { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
