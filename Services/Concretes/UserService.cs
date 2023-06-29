using AutoMapper;
using DataAccess.Db;
using DataAccess.Entities.UserEntity;
using DTOs.User;
using Helpers.Consts;
using Helpers.EncryptionMethods;
using Microsoft.EntityFrameworkCore;
using Services.Abstracts;

namespace Services.Concretes
{
    public class UserService : BaseService<UserDTO, User, UserDTO>, IUserService
    {
        public UserService(IMapper mapper, ApplicationDbContext dbContext) 
            : base(mapper, dbContext)
        {
        }

        public override UserDTO Create(UserDTO userDTO)
        {
            var user = _dbContext.Users.Where(u => u.Username == userDTO.Username);
            var role = _dbContext.Roles.Where(r => r.Name == RoleKeywords.UserRole).First();
            userDTO.RoleId = role.Id;

            if (user.Any())
            {
                throw new Exception("Username is taken!");
            }

            userDTO.Salt = Encryption.GenerateSalt();
            userDTO.Hash = Encryption.GenerateHash(userDTO.Password, userDTO.Salt);
            return base.Create(userDTO);
        }

        public UserDTO Login(UserDTO userDTO)
        {
            var user = _dbContext.Users.Where(u => u.Username == userDTO.Username)
                .Include(u => u.Role);
            if (user.Count() == 1)
            {
                var username = user.FirstOrDefault();
                var hash = Encryption.GenerateHash(userDTO.Password, username.Salt);

                if (hash == username.Hash)
                {
                    var model = _mapper.Map<User, UserDTO>(user.First());
                    return model;
                }
                else
                {
                    throw new Exception("Password is incorrect!");
                }
            }
            else
            {
                throw new Exception("Username is not found!");
            }
        }
    }
}