namespace FastFood.DataProcessor.Dto.Import
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Text;
	using System.Xml.Serialization;
	using Models;
	using Models.Enums;

    [XmlType("Order")]
    public class OrderDto
    {
        [XmlElement]
        public string Customer { get; set; }

        [XmlElement]
        public string Employee { get; set; }

        [XmlElement]
        public string DateTime { get; set; }

        [XmlElement]
        public string Type { get; set; }

        [XmlArray("Items")]
        public OrderItemDto[] Items { get; set; } 
    }
}
