namespace TeamBuilder.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Attributes;
    using Enums;

    public class User
    {
        [Key]
        [MinLength(0)]
        public int Id { get; set; }

        [Required]
        [StringLength(25, MinimumLength = 3)]
        public string Username { get; set; }

        [MaxLength(25)]
        public string FirstName { get; set; }

        [MaxLength(25)]
        public string LastName { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 6)]
        [Password(Digits = 1, UppercaseLetters = 1)]
        public string Password { get; set; }

        public Gender Gender { get; set; }

        [MinLength(0)]
        public int Age { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<Event> CreatedEvents { get; set; } = new List<Event>();

        public virtual ICollection<UserTeam> Teams { get; set; } = new List<UserTeam>();

        public virtual ICollection<Team> CreatedTeams { get; set; } = new List<Team>();

        public virtual ICollection<Invitation> Invitations { get; set; } = new List<Invitation>();

    }
}
