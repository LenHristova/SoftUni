namespace IRunes.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Album : BaseEntity<string>
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        [MaxLength(200)]
        public string CoverImageUrl { get; set; }

        public virtual ICollection<TrackAlbum> Tracks { get; set; } = new HashSet<TrackAlbum>();

        //public ICollection<UserAlbum> Users { get; set; } = new HashSet<UserAlbum>();
    }
}
