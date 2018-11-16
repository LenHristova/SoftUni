namespace Chushka.Common.Models.Orders
{
    using System;
    using System.Globalization;

    public class AllOrdersViewModel
    {
        public string Id { get; set; }

        public string ProductName { get; set; }

        public string ClientUserName { get; set; }

        public DateTime OrderedOn { get; set; }

        public string FormattedOrderedOn
            => this.OrderedOn.ToString("HH:mm dd/MM/yyyy", CultureInfo.InvariantCulture);
    }
}
