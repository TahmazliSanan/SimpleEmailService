using DataAccess.Entities.Base;
using DataAccess.Entities.RoleEntity;

namespace DataAccess.Entities.UserEntity
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Hash { get; set; }
        public string Salt { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}