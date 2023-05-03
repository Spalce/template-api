using AutoMapper;
using Template.Core.Models;

namespace Template.Api.Helpers;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Infrastructure.Models.Student, Student>().ReverseMap();
        CreateMap<Infrastructure.Models.Contact, Contact>().ReverseMap();
        CreateMap<Infrastructure.Models.AppUser, AppUser>().ReverseMap();
    }
}
