namespace Employees.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Employee
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50), MinLength(2)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50), MinLength(2)]
        public string LastName { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Salary { get; set; }

        public DateTime? Birthday { get; set; }

        [MaxLength(50), MinLength(2)]
        public string Address { get; set; }

        public int? ManagerId { get; set; }
        public virtual Employee Manager { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
