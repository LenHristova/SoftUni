namespace PetClinic.DataProcessor.Dtos
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Attributes;

    public class PassportDto
    {
        [SerialNumber]
        public string SerialNumber { get; set; }

        [Required]
        [MaxLength(13)]
        [PhoneNumber]
        public string OwnerPhoneNumber { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string OwnerName { get; set; }

        public string RegistrationDate { get; set; }
    }
}
