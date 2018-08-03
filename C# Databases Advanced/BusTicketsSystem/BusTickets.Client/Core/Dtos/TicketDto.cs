namespace BusTickets.Client.Core.Dtos
{
    public class TicketDto
    {
        public int Id { get; set; }

        public decimal Price { get; set; }

        public string Seat { get; set; }

        public virtual CustomerDto Customer { get; set; }

        public virtual TripDto Trip { get; set; }
    }
}
