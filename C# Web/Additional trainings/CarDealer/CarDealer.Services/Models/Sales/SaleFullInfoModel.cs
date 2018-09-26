namespace CarDealer.Services.Models.Sales
{
    using Cars;
    using Customers;

    public class SaleFullInfoModel
    {
        public decimal CarPrice { get; set; }

        public double Discount { get; set; }

        public CarWithPartsModel Car { get; set; }

        public CustomerModel Customer { get; set; }

        public decimal CarPriceWithDiscount => this.CarPrice * (1 - (decimal)this.Discount);
    }
}
