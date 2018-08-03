namespace CarDealer.Client.Dtos
{
	using System.Xml.Serialization;

    [XmlType("car")]
    public class CarInfoDto
    {
        [XmlIgnore]
        public int Id { get; set; }

        [XmlAttribute("make")]
        public string Make { get; set; }

        [XmlAttribute("model")]
        public string Model { get; set; }

        [XmlAttribute("travelled-distance")]
        public long TravelledDistance { get; set; }
    }
}
