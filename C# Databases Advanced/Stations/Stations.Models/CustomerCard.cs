namespace Stations.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using Enums;

    public class CustomerCard
    {
        //•	Id – integer, Primary Key
        //•	Name – text with max length 128 (required)
        //•	Age – integer between 0 and 120
        //•	Type – CardType enumeration with values: "Pupil", "Student", "Elder", "Debilitated", "Normal" (default: Normal)
        //•	BoughtTickets – Collection of type Ticket

        public int Id { get; set; }

        [Required]
        [MaxLength(128)]
        public string Name { get; set; }

        [Required]
        [Range(0, 120)]
        public int Age { get; set; }

        public CardType Type { get; set; } = CardType.Normal;

        public virtual ICollection<Ticket> BoughtTickets { get; set; } = new List<Ticket>();
    }
}
