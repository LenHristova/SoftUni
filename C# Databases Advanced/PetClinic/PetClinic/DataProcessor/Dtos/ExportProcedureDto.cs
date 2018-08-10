namespace PetClinic.DataProcessor.Dtos
{
    using System.Xml.Serialization;

    [XmlType("Procedure")]
    public class ExportProcedureDto
    {
        [XmlElement]
        public string Passport { get; set; }

        [XmlElement]
        public string OwnerNumber { get; set; }

        [XmlElement]
        public string DateTime { get; set; }

        [XmlArray]
        public AnimalAidDto[] AnimalAids { get; set; }

        [XmlElement]
        public decimal TotalPrice { get; set; }
    }
}
