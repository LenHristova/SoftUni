namespace PetClinic.DataProcessor.Dtos
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;
	using Attributes;

    [XmlType("Vet")]
    public class VetDto
    {
        [Required]
        [StringLength(40, MinimumLength = 3)]
        [XmlElement]
        public string Name { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        [XmlElement]
        public string Profession { get; set; }

        [Range(22, 65)]
        [XmlElement]
        public int Age { get; set; }

        [Required]
        [PhoneNumber]
        [XmlElement]
        public string PhoneNumber { get; set; }
    }
}
