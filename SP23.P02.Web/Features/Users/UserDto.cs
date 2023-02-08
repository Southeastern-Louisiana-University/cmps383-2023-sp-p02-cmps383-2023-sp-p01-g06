using SP23.P02.Web.Features.UserRoles;

namespace SP23.P02.Web.Features.Users
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public ICollection<UserRole> Roles { get; set; } = new List<UserRole>();
    }
}
