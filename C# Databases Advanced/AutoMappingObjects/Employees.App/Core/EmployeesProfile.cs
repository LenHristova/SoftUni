namespace Employees.App
{
    using AutoMapper;
    using Employees.DtoModels;
    using Employees.Models;

    internal class EmployeesProfile : Profile
    {
        public EmployeesProfile() 
        {
            CreateMap<Employee, EmployeeDto>()
                .ReverseMap();

            CreateMap<Employee, EmployeePersonalDto>()
                .ForMember(dto => dto.Birthday,
                    opt => opt.MapFrom(src => src.Birthday.HasValue ? src.Birthday.Value.ToString("d MMM yyyy") : null))
                .ReverseMap();

            CreateMap<Employee, ManagerDto>()
                .ReverseMap();

            CreateMap<Employee, EmployeeManagerInfoDto>()
                .ReverseMap();

            CreateMap<Employee, Employee>();

        }
    }
}
