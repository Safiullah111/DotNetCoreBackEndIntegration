using API.Work.Application.Contract.Services.Permissions;
using API.Work.Application.Contract.Services.Roles;
using API.Work.Application.Contract.Services.Users;
using API.Work.Domain.Services.Permissions;
using API.Work.Domain.Services.Roles;
using API.Work.Domain.Services.Users;
using AutoMapper;


namespace API.Work.Application.Common.Mapping
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<CreateUserDto, User>();
            CreateMap<Role, RoleDto>(); 
            CreateMap<Permission, PermissionDto>();
            
        }
    }
}
