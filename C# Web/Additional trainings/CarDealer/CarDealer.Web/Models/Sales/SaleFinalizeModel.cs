namespace CarDealer.Web.Models.Sales
{
    using System.ComponentModel.DataAnnotations;
    using Services.Models.Cars;
    using Services.Models.Customers;

    public class SaleFinalizeModel
    {
        public CustomerSaleInfoModel Customer { get; set; }

        public CarPriceModel Car { get; set; }

        public int Discount { get; set; }

        public int AdditionalYoungDriverDiscount => 5;

        public int FinalDiscount() 
            => this.Customer.IsYoungDriver
                ? this.Discount + this.AdditionalYoungDriverDiscount
                : this.Discount;

        [Display(Name = "Final car price")]
        public decimal FinalPrice() 
            => this.Car.Price * (1 - this.FinalDiscount() / 100m);

        //private int GetFinalDiscount()
        //    => Customer.IsYoungDriver
        //        ? this.Discount + this.AdditionalYoungDriverDiscount
        //        : this.Discount;
    }
}
