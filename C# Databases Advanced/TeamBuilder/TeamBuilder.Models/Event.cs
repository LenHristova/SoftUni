namespace TeamBuilder.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Event
    {
        [Key]
        [MinLength(0)]
        public int Id { get; set; }

        [Required]
        [MaxLength(25)]
        public string Name { get; set; }

        [MaxLength(250)]
        public string Description { get; set; }

        [Column(TypeName = "SMALLDATETIME")]
        public DateTime StartDate { get; set; }

        [Column(TypeName = "SMALLDATETIME")]
        public DateTime EndDate { get; set; }

        [MinLength(0)]
        public int CreatorId { get; set; }
        public virtual User Creator { get; set; }

        public virtual ICollection<TeamEvent> ParticipatingTeams { get; set; } = new List<TeamEvent>();
    }
}
