namespace CarDealer.Client.Dtos
{
	using System.Xml.Serialization;

    [XmlType("customer")]
    public class CustomerPurchasesInfoDto
    {
        [XmlAttribute("full-name")]
        public string Name { get; set; }

        [XmlAttribute("bought-cars")]
        public int BoughtCars { get; set; }

        [XmlAttribute("spent-money")]
        public string SpendMoney { get; set; }
    }
}
