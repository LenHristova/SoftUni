using System.Collections.Generic;

namespace CarDealer.Web.Models.Customers
{
    using Services.Models.Customers;

    public class OrderedCustomersModel
    {
        public IEnumerable<CustomerModel> Customers { get; set; }

        public string OrderType { get; set; }
    }
}
