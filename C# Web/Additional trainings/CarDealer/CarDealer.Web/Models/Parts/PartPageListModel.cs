namespace CarDealer.Web.Models.Parts
{
    using Services.Models.Parts;
    using System.Collections.Generic;

    public class PartPageListModel
    {
        public IEnumerable<PartModel> Parts { get; set; }

        public Pagination Pagination { get; set; }
    }
}
