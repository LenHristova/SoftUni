namespace CarDealer.Services.Models.Sales
{
    using Cars;
    using Customers;

    public class SaleModel
    {
        public int Id { get; set; }

        public decimal CarPrice { get; set; }

        public double Discount { get; set; }

        public CarBaseModel Car { get; set; }

        public CustomerBaseModel Customer { get; set; }

        public decimal CarPriceWithDiscount => this.CarPrice * (1 - (decimal) this.Discount);
    }
}
