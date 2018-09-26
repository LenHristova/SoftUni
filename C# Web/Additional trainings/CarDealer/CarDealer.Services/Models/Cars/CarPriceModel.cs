namespace CarDealer.Services.Models.Cars
{
    using System.ComponentModel.DataAnnotations;

    public class CarPriceModel
    {
        public int Id { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        [Display(Name = "Car price")]
        public decimal Price { get; set; }
    }
}
