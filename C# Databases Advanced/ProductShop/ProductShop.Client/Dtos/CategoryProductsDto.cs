namespace ProductShop.Client.Dtos
{
    using System.Xml.Serialization;

    [XmlType("category")]
    public class CategoryProductsDto
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlElement("products-count")]
        public int ProductsCount { get; set; }

        [XmlElement("average-price")]
        public string ProductsAvgPrice { get; set; }

        [XmlElement("total-revenue")]
        public string ProductsTotalRevenue { get; set; }
    }
}
