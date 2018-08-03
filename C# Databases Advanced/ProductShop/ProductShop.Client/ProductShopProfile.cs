namespace ProductShop.Client
{
    using AutoMapper;
	using Dtos;
    using Models;
    using System.Linq;

    public class ProductShopProfile : Profile
    {
        public ProductShopProfile()
        {
            CreateMap<UserDto, User>().ReverseMap();

            CreateMap<ProductDto, Product>().ReverseMap();

            CreateMap<CategoryDto, Category>();

            CreateMap<Product, ProductWithBuyerDto>()
                .ForMember(dest => dest.Buyer, 
                    opt => opt.MapFrom(src => $"{src.Buyer.FirstName} {src.Buyer.LastName}".Trim()));

            CreateMap<User, UserWithSoldItemsDto>();

            CreateMap<Category, CategoryProductsDto>()
                .ForMember(dest => dest.ProductsCount, 
                    opt => opt.MapFrom(src => src.Products.Count))
                .ForMember(dest => dest.ProductsAvgPrice,
                    opt => opt.MapFrom(src => src.Products.Any()
                        ? src.Products.Average(p => p.Product.Price)
                        : 0))
                .ForMember(dest => dest.ProductsTotalRevenue,
                    opt => opt.MapFrom(src => src.Products.Sum(p => p.Product.Price)));
        }
    }
}
