namespace SoftJail.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class OfficerPrisoner
    {
        //•	PrisonerId – integer, Primary Key
        //•	Prisoner – the officer’s prisoner(required)
        //•	OfficerId – integer, Primary Key
        //•	Officer – the prisoner’s officer(required)

        [Required]
        public int PrisonerId { get; set; }
        [Required]
        public Prisoner Prisoner { get; set; }

        [Required]
        public int OfficerId { get; set; }
        [Required]
        public Officer Officer { get; set; }
    }
}
