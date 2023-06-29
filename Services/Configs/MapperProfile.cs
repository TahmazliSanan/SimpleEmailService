using AutoMapper;
using DataAccess.Entities.UserEntity;
using DTOs.User;

namespace Services.Configs
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();
        }
    }
}