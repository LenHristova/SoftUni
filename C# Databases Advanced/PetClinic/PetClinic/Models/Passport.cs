namespace PetClinic.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Attributes;

    public class Passport
    {
        [Key]
        [SerialNumber]
        [Column(TypeName = "CHAR(10)")]
        public string SerialNumber { get; set; }

        public virtual Animal Animal { get; set; }

        [Required]
        [MaxLength(13)]
        [PhoneNumber]
        public string OwnerPhoneNumber { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string OwnerName { get; set; }

        public DateTime RegistrationDate { get; set; }
    }
}
