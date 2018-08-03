namespace BusTickets.Client.Core.Dtos
{
    public class TripDto
    {
        //public int Id { get; set; }

        public string DepartureTime { get; set; }

        public string ArrivalTime { get; set; }

        public string Status { get; set; }

        public StationBaseDto OriginStation { get; set; }

        public StationBaseDto DestinationStation { get; set; }

        public CompanyBaseDto Company { get; set; }
    }
}
