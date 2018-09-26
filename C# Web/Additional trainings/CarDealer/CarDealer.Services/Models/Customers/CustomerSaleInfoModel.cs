namespace CarDealer.Services.Models.Customers
{
    using System.ComponentModel.DataAnnotations;

    public class CustomerSaleInfoModel
    {
        public int Id { get; set; }

        [Display(Name = "Customer name")]
        public string Name { get; set; }

        public bool IsYoungDriver { get; set; }
    }
}
