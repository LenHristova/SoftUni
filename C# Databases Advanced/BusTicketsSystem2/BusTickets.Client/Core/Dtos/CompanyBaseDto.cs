namespace BusTickets.Client.Core.Dtos
{
    public class CompanyBaseDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public CountryDto Nationality { get; set; }
    }
}
