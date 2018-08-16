namespace SoftJail.DataProcessor.ExportDto
{
	using System;
	using System.Collections.Generic;
	using System.Text;
	using System.Xml.Serialization;

    [XmlType("Message")]
    public class MessageDto
    {
        public string Description { get; set; }
    }
}
