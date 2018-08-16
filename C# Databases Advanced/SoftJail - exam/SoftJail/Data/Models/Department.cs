namespace SoftJail.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class Department
    {
        //•	Id – integer, Primary Key
        //•	Name – text with min length 3 and max length 25 (required)
        //•	Cells - collection of type Cell

        public int Id { get; set; }

        [Required]
        [StringLength(25, MinimumLength = 3)]
        public string Name { get; set; }

        public virtual ICollection<Cell> Cells { get; set; } = new List<Cell>();

        public virtual ICollection<Officer> Officers { get; set; } = new List<Officer>();
    }
}
