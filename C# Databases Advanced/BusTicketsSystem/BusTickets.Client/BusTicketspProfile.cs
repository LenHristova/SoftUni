namespace BusTickets.Client
{
    using AutoMapper;
	using Core.Dtos;
	using Models;

    public class BusTicketspProfile : Profile
    {
        public BusTicketspProfile()
        {
            CreateMap<Customer, CustomerDto>()
                .ForMember(dest => dest.DateOfBirth, from => from.MapFrom(p => p.DateOfBirth.ToString("d MMM yyyy")))
                .ForMember(dest => dest.Gender, from => from.MapFrom(p => p.Gender.ToString()))
                .ReverseMap();

            CreateMap<BankAccount, BankAccountDto>()
                .ReverseMap();

            CreateMap<Country, CountryDto>()
                .ReverseMap();

            CreateMap<Company, CompanyBaseDto>()
                .ReverseMap();

            CreateMap<Company, CompanyReviewsDto>()
                .ReverseMap();

            CreateMap<Review, ReviewBaseDto>()
                .ReverseMap();

            CreateMap<Review, ReviewDto>()
                .ForMember(dest => dest.PublishedDateTime, from => from.MapFrom(p => p.PublishedDateTime.Value.ToString("d MMM yyyy")))
                .ForMember(dest => dest.CustomerFullName, from => from.MapFrom(p => p.Customer.FirstName + " " + p.Customer.LastName));

            CreateMap<Station, StationBaseDto>()
                .ReverseMap();

            CreateMap<Station, StationDto>()
                .ReverseMap();

            CreateMap<Town, TownBaseDto>()
                .ReverseMap();

            CreateMap<Trip, TripDto>()
                .ForMember(dest => dest.DepartureTime, from => from.MapFrom(p => p.DepartureTime.ToString("f")))
                .ForMember(dest => dest.ArrivalTime, from => from.MapFrom(p => p.ArrivalTime.ToString("f")))
                .ForMember(dest => dest.Status, from => from.MapFrom(p => p.Status.ToString()))
                .ReverseMap();

            CreateMap<Ticket, TicketDto>()
                .ReverseMap();
        }
    }
}
