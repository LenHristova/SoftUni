namespace SoftJail.DataProcessor.ExportDto
{
	using System;
	using System.Collections.Generic;
	using System.Text;
	using System.Xml.Serialization;

    [XmlType("Prisoner")]
    public class PrisonerExportDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string IncarcerationDate { get; set; }

        public MessageDto[] EncryptedMessages { get; set; }
    }
}
