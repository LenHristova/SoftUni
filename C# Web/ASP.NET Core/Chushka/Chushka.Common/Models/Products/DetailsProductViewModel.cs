namespace Chushka.Common.Models.Products
{
    using Data.Models.Enums;

    public class DetailsProductViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public ProductType Type { get; set; }
    }
}
