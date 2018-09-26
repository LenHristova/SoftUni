namespace CarDealer.Web.Models.Cars
{
    using System.Collections.Generic;
    using Services.Models.Cars;

    public class OrderedCarsFromMake
    {
        public string Make { get; set; }

        public IEnumerable<CarModel> Cars { get; set; }
    }
}
