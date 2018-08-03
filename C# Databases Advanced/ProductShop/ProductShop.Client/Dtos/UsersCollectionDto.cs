namespace ProductShop.Client.Dtos
{
    using System.Xml.Serialization;

    [XmlRoot("users")]
    public class UsersCollectionDto
    {
        [XmlAttribute("count")]
        public int Count { get; set; }

        [XmlElement("user")]
        public UserWithProductsDto[] UserWithProducts { get; set; }
    }
}
