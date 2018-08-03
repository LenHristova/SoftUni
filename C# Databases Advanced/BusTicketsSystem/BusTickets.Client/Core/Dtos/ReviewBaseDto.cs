namespace BusTickets.Client.Core.Dtos
{
    public class ReviewBaseDto
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public double Grade { get; set; }

        public CompanyBaseDto Company { get; set; }

        public CustomerDto Customer { get; set; }
    }
}
