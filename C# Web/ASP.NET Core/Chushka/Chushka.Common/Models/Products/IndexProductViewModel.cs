namespace Chushka.Common.Models.Products
{
    using System;

    public class IndexProductViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string SubstringedDescription 
            => this.Description.Substring(0, Math.Min(this.Description.Length, 50)) + "...";
    }
}
