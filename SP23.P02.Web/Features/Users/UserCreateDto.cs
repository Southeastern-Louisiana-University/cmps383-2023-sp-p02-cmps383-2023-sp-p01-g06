using SP23.P02.Web.Features.UserRoles;

namespace SP23.P02.Web.Features.Users
{
    public class UserCreateDto
    {
        public string UserName { get; set; }
        public ICollection<UserRole> Roles { get; set; }
        public string Password { get; set; }
    }
}
