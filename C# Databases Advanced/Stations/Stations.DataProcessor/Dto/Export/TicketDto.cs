namespace Stations.DataProcessor.Dto.Export
{
    using System.Xml.Serialization;

    [XmlType("Ticket")]
    public class TicketDto
    {
        [XmlElement]
        public string OriginStation { get; set; }

        [XmlElement]
        public string DestinationStation { get; set; }

        [XmlElement]
        public string DepartureTime { get; set; }
    }
}
