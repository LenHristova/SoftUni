namespace CarDealer.Client
{
    using AutoMapper;
    using Models;
    using Dtos;

    internal class CarDealerProfile : Profile
    {
        public CarDealerProfile()
        {
            CreateMap<SupplierDto, Supplier>();
            CreateMap<PartDto, Part>();
            CreateMap<CarDto, Car>();
            CreateMap<CustomerDto, Customer>();
        }
    }
}