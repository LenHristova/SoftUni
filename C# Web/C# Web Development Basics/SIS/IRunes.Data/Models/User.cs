namespace IRunes.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class User : BaseEntity<string>
    {
        [Required]
        [MaxLength(20)]
        public string Username { get; set; }

        [Required]
        [StringLength(200)]
        public string PasswordHash { get; set; }

        [Required]
        [MaxLength(20)]
        public string Email { get; set; }

        //public ICollection<UserAlbum> Albums { get; set; } = new HashSet<UserAlbum>();
    }
}
