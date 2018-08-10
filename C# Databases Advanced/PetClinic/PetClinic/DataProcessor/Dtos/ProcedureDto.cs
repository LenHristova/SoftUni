namespace PetClinic.DataProcessor.Dtos
{
	using System.Xml.Serialization;

    [XmlType("Procedure")]
    public class ProcedureDto
    {
        [XmlElement]
        public string Vet { get; set; }

        [XmlElement]
        public string Animal { get; set; }
        
        [XmlElement]
        public string DateTime { get; set; }

        [XmlArray]
        public AnimalAidDto[] AnimalAids { get; set; }
    }
}
