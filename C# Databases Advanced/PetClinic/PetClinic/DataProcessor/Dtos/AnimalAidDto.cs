namespace PetClinic.DataProcessor.Dtos
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;

    [XmlType("AnimalAid")]
    public class AnimalAidDto
    {
        [Required]
        [StringLength(30, MinimumLength = 3)]
        [XmlElement]
        public string Name { get; set; }

        [Range(0.01, double.PositiveInfinity)]
        [XmlElement]
        public decimal Price { get; set; }
    }
}
