using DTOs.Base;

namespace DTOs.User
{
    public class UserDTO : BaseDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Hash { get; set; }
        public string Salt { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
}