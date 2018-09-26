namespace CarDealer.Services.Models.Customers
{
    using System.Collections.Generic;
    using Sales;

    public class CustomerBoughtCarsModel
    {
        public string Name { get; set; }

        public int BoughtCars { get; set; }

        public IEnumerable<SaleBaseModel> SaleInfo { get; set; }
    }
}
