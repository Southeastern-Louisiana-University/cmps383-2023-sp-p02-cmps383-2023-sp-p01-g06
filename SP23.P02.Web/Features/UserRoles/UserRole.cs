using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SP23.P02.Web.Features.Roles;
using SP23.P02.Web.Features.Users;

namespace SP23.P02.Web.Features.UserRoles
{
    public class UserRole : IdentityUserRole<int>
    {
        public virtual User? Users { get; set; }
        public virtual Role? Roles { get; set; }

        
    }

}
