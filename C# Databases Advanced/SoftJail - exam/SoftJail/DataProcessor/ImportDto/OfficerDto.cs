namespace SoftJail.DataProcessor.ImportDto
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.Text;
	using System.Xml;
	using System.Xml.Serialization;
	using Data.Models;
	using Data.Models.Enums;

    [XmlType("Officer")]
    public class OfficerDto
    {
        [Required]
        [StringLength(30, MinimumLength = 3)]
        [XmlElement]
        public string Name { get; set; }

        [Required]
        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        [XmlElement]
        public decimal Money { get; set; }

        [Required]
        [XmlElement]
        public string Position { get; set; }

        [Required]
        [XmlElement]
        public string Weapon { get; set; }

        [Required]
        [XmlElement]
        public int DepartmentId { get; set; }

        [XmlArray("Prisoners")]
        public OfficerPrisonerDto[] Prisoners { get; set; }
    }
}
