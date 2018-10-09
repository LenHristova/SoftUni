namespace IRunes.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Track : BaseEntity<string>
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        [MaxLength(200)]
        public string VideoUrl { get; set; }

        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal Price { get; set; }

        public virtual ICollection<TrackAlbum> Albums { get; set; } = new HashSet<TrackAlbum>();
    }
}
