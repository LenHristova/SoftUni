namespace CarDealer.Services.Models.Suppliers
{
    using Parts;
    using System.Collections.Generic;

    public class SupplierWithPartsModel
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public ICollection<SupplierPartModel> Parts { get; set; }
    }
}
