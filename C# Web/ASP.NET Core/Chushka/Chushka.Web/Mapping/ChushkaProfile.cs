namespace Chushka.Web.Mapping
{
    using AutoMapper;
    using Common.Models.Orders;
    using Common.Models.Products;
    using Data.Models;

    public class ChushkaProfile : Profile
    {
        public ChushkaProfile()
        {
            CreateMap<Product, IndexProductViewModel>();
            CreateMap<Product, EditProductInputModel>();
            CreateMap<Product, DeleteProductInputModel>();
            CreateMap<Order, AllOrdersViewModel>();
        }
    }
}
