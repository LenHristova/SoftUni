﻿namespace ProductShop.Client.Dtos
{
    using System.Xml.Serialization;

    [XmlType("user")]
    public class UserWithProductsDto 
    {
        [XmlAttribute("first-name")]
        public string FirstName { get; set; }

        [XmlAttribute("last-name")]
        public string LastName { get; set; }

        [XmlAttribute("age")]
        public string Age { get; set; }

        [XmlElement("sold-products")]
        public ProductsCollectionDto SoldProducts { get; set; }
    }
}
