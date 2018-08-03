namespace ProductShop.Client.Dtos
{
    using System.Xml.Serialization;

    [XmlType("sold-products")]
    public class ProductsCollectionDto
    {
        [XmlAttribute("count")]
        public int Count { get; set; }

        [XmlElement("product")]
        public ProductDto[] Products { get; set; }
    }
}
