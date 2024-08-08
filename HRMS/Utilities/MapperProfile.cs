using AutoMapper;
using HRMS.Models;
using HRMS.Models.EntitesObjects;

namespace HRMS.Utilities
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UserDto, Users>().ReverseMap();
            CreateMap<SaveUserDto, Users>().ReverseMap();
        }
    }
}
