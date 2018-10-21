namespace SoftUniCopy.Web.Mapping
{
    using Areas.Admin.Models.Users;
    using AutoMapper;
    using SoftUniCopy.Models;

    public class SoftUniCopyProfile : Profile
    {
        public SoftUniCopyProfile()
        {
            this.CreateMap<User, User>();
            this.CreateMap<User, UserListingModel>();
            this.CreateMap<User, UserDetailsModel>();
        }
    }
}
