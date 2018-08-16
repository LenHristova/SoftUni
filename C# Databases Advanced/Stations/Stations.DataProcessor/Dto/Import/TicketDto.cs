namespace Stations.DataProcessor.Dto.Import
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;

    [XmlType("Ticket")]
    public class TicketDto
    {
        [Required]
        [XmlAttribute("price")]
        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal Price { get; set; }

        //max length of 8 which combines seating class abbreviation plus a positive integer
        [Required]
        [StringLength(8), MinLength(3)]
        [RegularExpression("^.{2}[0-9]{1,6}$")]
        [XmlAttribute("seat")]
        public string Seat { get; set; }

        [Required]
        [XmlElement("Trip")]
        public TicketTripDto Trip { get; set; }

        [XmlElement("Card", IsNullable = true)]
        public TicketCardDto Card { get; set; }
    }
}
