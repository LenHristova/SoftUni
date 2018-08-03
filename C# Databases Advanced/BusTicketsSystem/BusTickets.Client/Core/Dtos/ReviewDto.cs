namespace BusTickets.Client.Core.Dtos
{
    public class ReviewDto
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public double Grade { get; set; }

        public string CustomerFullName { get; set; }

        public string PublishedDateTime { get; set; }
    }
}
