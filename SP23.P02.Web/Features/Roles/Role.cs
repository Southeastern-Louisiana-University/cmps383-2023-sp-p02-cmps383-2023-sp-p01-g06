using Microsoft.AspNetCore.Identity;
using SP23.P02.Web.Features.UserRoles;

namespace SP23.P02.Web.Features.Roles
{
    public class Role : IdentityRole<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<UserRole> Users { get; set; } = new List<UserRole>();
    }
}
