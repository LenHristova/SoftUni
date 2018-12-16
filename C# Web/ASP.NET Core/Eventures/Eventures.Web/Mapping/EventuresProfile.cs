namespace Eventures.Web.Mapping
{
    using AutoMapper;
    using Common.Models.Events;
    using Common.Models.Orders;
    using Data.Models;
    using System.Globalization;

    public class EventuresProfile : Profile
    {
        public EventuresProfile()
        {
            CreateMap<Event, EventListViewModel>();

            CreateMap<Order, UserOrderListViewModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Event.Name))
                .ForMember(dest => dest.Start,
                    opt => opt.MapFrom(src => src.Event.Start.ToLocalTime().ToString("dd-MMM-yy hh:mm:ss", CultureInfo.InvariantCulture)))
                .ForMember(dest => dest.End,
                    opt => opt.MapFrom(src => src.Event.End.ToLocalTime().ToString("dd-MMM-yy hh:mm:ss", CultureInfo.InvariantCulture)));

            CreateMap<Order, AllOrderListViewModel>()
                .ForMember(dest => dest.Event, opt => opt.MapFrom(src => src.Event.Name))
                .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer.UserName))
                .ForMember(dest => dest.OrderedOn,
                    opt => opt.MapFrom(src => src.OrderedOn.ToLocalTime().ToString("dd-MMM-yy hh:mm:ss", CultureInfo.InvariantCulture)));

            CreateMap<CreateEventInputModel, Event>()
                .ForMember(dest => dest.Start, opt => opt.MapFrom(src => src.Start.ToUniversalTime()))
                .ForMember(dest => dest.End, opt => opt.MapFrom(src => src.End.ToUniversalTime()));
        }
    }
}
