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
            CreateMap<Event, EventListViewModel>()
                .ForMember(dest => dest.StartToString,
                    m => m.MapFrom(src => src.Start.ToLocalTime().ToString("dd-MMM-yy hh:mm:ss", CultureInfo.InvariantCulture)))
                .ForMember(dest => dest.EndToString,
                    m => m.MapFrom(src => src.End.ToLocalTime().ToString("dd-MMM-yy hh:mm:ss", CultureInfo.InvariantCulture)));

            CreateMap<Order, UserOrderListViewModel>()
                .ForMember(dest => dest.Name, m => m.MapFrom(src => src.Event.Name))
                .ForMember(dest => dest.Start,
                    m => m.MapFrom(src => src.Event.Start.ToLocalTime().ToString("dd-MMM-yy hh:mm:ss", CultureInfo.InvariantCulture)))
                .ForMember(dest => dest.End,
                    m => m.MapFrom(src => src.Event.End.ToLocalTime().ToString("dd-MMM-yy hh:mm:ss", CultureInfo.InvariantCulture)));

            CreateMap<Order, AllOrderListViewModel>()
                .ForMember(dest => dest.Event, m => m.MapFrom(src => src.Event.Name))
                .ForMember(dest => dest.Customer, m => m.MapFrom(src => src.Customer.UserName))
                .ForMember(dest => dest.OrderedOn,
                    m => m.MapFrom(src => src.OrderedOn.ToLocalTime().ToString("dd-MMM-yy hh:mm:ss", CultureInfo.InvariantCulture)));

            CreateMap<CreateEventInputModel, Event>()
                .ForMember(dest => dest.Start, m => m.MapFrom(src => src.Start.ToUniversalTime()))
                .ForMember(dest => dest.End, m => m.MapFrom(src => src.End.ToUniversalTime()));
        }
    }
}
