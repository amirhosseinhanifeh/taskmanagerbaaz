using AutoMapper;
using MS.Services.TaskCatalog.Contract.Users.Commands;
using MS.Services.TaskCatalog.Contract.Users.Dtos;
using MS.Services.TaskCatalog.Domain.Tasks;
using MS.Services.TaskCatalog.Domain.Users;

namespace MS.Services.TaskCatalog.Application.Users;
public class UserMappers : Profile
{
    public UserMappers()
    {
        CreateMap<User, UserDto>()
            .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name));

        CreateMap<UserSelection, UserDto>()
            .ForMember(x => x.Id, opt => opt.MapFrom(x => x.SelectUserId))
            .ForMember(x => x.Name, opt => opt.MapFrom(x => x.SelectUser.Name))
            .ForMember(x => x.Avatar, opt => opt.MapFrom(x => x.SelectUser.Avatar))
    ;
        CreateMap<CreateUserCommand, User>();

        //CreateMap<CreateUserRequest, CreateUserCommand>()
        //    .ConstructUsing(req => new CreateUserCommand(
        //        req.Name ));
    }
}