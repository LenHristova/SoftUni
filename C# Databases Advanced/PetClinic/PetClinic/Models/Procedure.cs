namespace PetClinic.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public class Procedure
    {
        public int Id { get; set; }

        public int AnimalId { get; set; }
        public virtual Animal Animal { get; set; }

        public int VetId { get; set; }
        public virtual Vet Vet { get; set; }

        public DateTime DateTime { get; set; }

        public virtual ICollection<ProcedureAnimalAid> ProcedureAnimalAids  { get; set; } = new List<ProcedureAnimalAid>();

        [NotMapped]
        public decimal Cost => this.ProcedureAnimalAids.Sum(pa => pa.AnimalAid.Price);
    }
}
