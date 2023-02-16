using SP23.P02.Web.Features.UserRoles;

namespace SP23.P02.Web.Features.Users
{
    public class UserDto
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public ICollection<string> Roles { get; set; }
    }
}
