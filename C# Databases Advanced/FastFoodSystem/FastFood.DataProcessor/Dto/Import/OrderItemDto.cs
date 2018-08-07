namespace FastFood.DataProcessor.Dto.Import
{
	using System.Xml.Serialization;

    [XmlType("Item")]
    public class OrderItemDto
    {
        [XmlElement]
        public string Name { get; set; }

        [XmlElement]
        public int Quantity { get; set; }
    }
}
