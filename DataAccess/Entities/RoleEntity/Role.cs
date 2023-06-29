using DataAccess.Entities.Base;
using DataAccess.Entities.UserEntity;

namespace DataAccess.Entities.RoleEntity
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }
        public List<User> Users { get; set; } = new List<User>();
    }
}