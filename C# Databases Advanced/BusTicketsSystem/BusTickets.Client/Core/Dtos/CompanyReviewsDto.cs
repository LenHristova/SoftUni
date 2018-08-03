namespace BusTickets.Client.Core.Dtos
{
    using System.Collections.Generic;

    public class CompanyReviewsDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<ReviewDto> Reviews { get; set; }
    }
}
