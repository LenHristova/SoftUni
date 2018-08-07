namespace TeamBuilder.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Invitation
    {
        [Key]
        [MinLength(0)]
        public int Id { get; set; }

        [MinLength(0)]
        public int InvitedUserId { get; set; }
        public virtual User InvitedUser { get; set; }

        [MinLength(0)]
        public int TeamId { get; set; }
        public virtual Team Team { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
