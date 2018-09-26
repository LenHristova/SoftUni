namespace CarDealer.Web.Models.Suppliers
{
    using System.Collections.Generic;
    using Services.Models.Enums;
    using Services.Models.Suppliers;

    public class SuppliersByIsImporterOrLocal
    {
        public SupplierType Type { get; set; }

        public IEnumerable<SupplierModel> Suppliers { get; set; }
    }
}
