namespace CarDealer.Client.Dtos
{
	using System.Xml.Serialization;

    [XmlType("part")]
    public class PartInfoDto
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("price")]
        public decimal Price { get; set; }
    }
}
