namespace Stations.DataProcessor.Dto.Import
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;

    [XmlType("Trip")]
    public class TicketTripDto
    {
        [Required]
        [XmlElement]
        public string OriginStation { get; set; }

        [Required]
        [XmlElement]
        public string DestinationStation { get; set; }

        [Required]
        [XmlElement]
        public string DepartureTime { get; set; }
    }
}
