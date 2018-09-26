namespace CarDealer.Services.Models.Cars
{
    using Parts;
    using System.Collections.Generic;

    public class CarWithPartsModel
    {
        public string Make { get; set; }

        public string Model { get; set; }

        public long TraveledDistance { get; set; }

        public ICollection<PartModel> Parts { get; set; }
    }
}
