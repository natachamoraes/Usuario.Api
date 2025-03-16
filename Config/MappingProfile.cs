using AutoMapper;
using Usuario.Api.Entity;
using Usuario.Api.Model;

namespace Usuario.Api.Config;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, CreateUserRequestDto>().ReverseMap();
        CreateMap<User, UpdateUserRequestDto>().ReverseMap();



    }
}