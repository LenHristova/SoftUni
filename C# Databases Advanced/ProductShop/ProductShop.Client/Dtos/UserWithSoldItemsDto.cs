namespace ProductShop.Client.Dtos
{
    using System.Xml.Serialization;

    [XmlType("user")]
    public class UserWithSoldItemsDto
    {
        [XmlAttribute("first-name")]
        public string FirstName { get; set; }

        [XmlAttribute("last-name")]
        public string LastName { get; set; }

        //[XmlAttribute("age")]
        //public int Age { get; set; }

        [XmlArray("sold-products")]
        public ProductDto[] SoldProducts { get; set; }
    }
}
