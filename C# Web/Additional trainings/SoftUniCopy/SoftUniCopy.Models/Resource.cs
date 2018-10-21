namespace SoftUniCopy.Models
{
    using System.ComponentModel.DataAnnotations;
    using Enums;

    public class Resource
    {
        public int Id { get; set; }

        public int LectureId { get; set; }
        public Lecture Lecture { get; set; }

        public ResourceType Type { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Url { get; set; }

        public int Order { get; set; }
    }
}