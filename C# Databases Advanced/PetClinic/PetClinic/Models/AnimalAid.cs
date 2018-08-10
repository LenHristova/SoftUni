namespace PetClinic.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AnimalAid
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Name { get; set; }

        [Range(0.01, double.PositiveInfinity)]
        public decimal Price { get; set; }

        public virtual ICollection<ProcedureAnimalAid> AnimalAidProcedures { get; set; } = new List<ProcedureAnimalAid>();

    }
}
