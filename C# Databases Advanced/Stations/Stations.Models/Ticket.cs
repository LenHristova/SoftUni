﻿namespace Stations.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class Ticket
    {
        //•	Id – integer, Primary Key
        //•	Price – decimal value of the ticket(required, non-negative)
        //•	SeatingPlace – text with max length of 8 which combines seating class abbreviation plus a positive integer(required)
        //•	TripId – integer(required)
        //•	Trip – the trip for which the ticket is for (required)
        //•	CustomerCardId – integer(optional)
        //•	CustomerCard – reference to the ticket’s buyer

        public int Id { get; set; }

        [Required]
        [Range(typeof(decimal), "0", "79228162514264337593543950335M")]
        public decimal Price { get; set; }

        [Required]
        [StringLength(8), MinLength(3)]
        [RegularExpression("^.{2}[0-9]{1,6}$")]
        //max length of 8 which combines seating class abbreviation plus a positive integer
        public string SeatingPlace { get; set; }

        [Required]
        public int TripId { get; set; }
        [Required]
        public virtual Trip Trip { get; set; }

        public int? CustomerCardId { get; set; }
        public virtual CustomerCard CustomerCard { get; set; }
    }
}
