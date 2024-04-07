using AutoMapper;
using Clean.Domain.Entities.Mongo;
using Clean.Domain.Entities.SQL;

namespace Clean.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        /*CreateMap<CreateUserCommand, User>()
            .ForMember(dest=>dest.UserName,opt=>opt.MapFrom(src=>src.UserName.ToLower()))
            .ForMember(dest=>dest.Email,opt=>opt.MapFrom(src=> src.Email!=null?src.Email.ToLower():null))
            .ForMember(dest=>dest.PasswordHash,opt=>opt.MapFrom(src=> src.Password!=null? BCrypt.Net.BCrypt.HashPassword(src.Password)  :null))
            .ReverseMap();*/
    }
}