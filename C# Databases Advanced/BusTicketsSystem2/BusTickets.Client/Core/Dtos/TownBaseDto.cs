namespace BusTickets.Client.Core.Dtos
{
    public class TownBaseDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public CountryDto Country { get; set; }
    }
}
