namespace CarDealer.Web.Models.Sales
{
    using System.Collections.Generic;
    using Services.Models.Sales;

    public class SalesByPredicateModel
    {
        public IEnumerable<SaleModel> Sales { get; set; }

        public string SaleType { get; set; }
    }
}
