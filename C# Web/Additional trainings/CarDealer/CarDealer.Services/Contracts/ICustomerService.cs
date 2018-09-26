namespace CarDealer.Services.Contracts
{
    using System.Collections.Generic;
    using Models.Customers;
    using Models.Enums;

    public interface ICustomerService
    {
        IEnumerable<CustomerModel> OrderedByBirthDate(OrderType order);

        CustomerBoughtCarsModel ById(int? id);
    }
}
