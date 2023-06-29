using DataAccess.Entities.UserEntity;
using DTOs.User;

namespace Services.Abstracts
{
    public interface IUserService : IBaseService<UserDTO, User, UserDTO>
    {
        UserDTO Login(UserDTO userDTO);
    }
}