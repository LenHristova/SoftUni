namespace BusTickets.Client.Core.Dtos
{
    public class StationBaseDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public TownBaseDto Town { get; set; }
    }
}
