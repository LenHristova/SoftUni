namespace CarDealer.Client.Dtos
{
	using System.Xml.Serialization;

    [XmlType("sale")]
    public class SaleDto
    {
        [XmlElement("car")]
        public CarInfoDto Car { get; set; }

        [XmlElement("customer-name")]
        public string CustomerName { get; set; }

        [XmlElement("discount")]
        public double Discount { get; set; }

        [XmlElement("price")]
        public decimal Price { get; set; }

        [XmlElement("price-with-discount")]
        public decimal PriceWithDiscount { get; set; }
    }
}
