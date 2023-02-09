using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SP23.P02.Web.Features.Roles;
using SP23.P02.Web.Features.Users;
using System.ComponentModel.DataAnnotations.Schema;

namespace SP23.P02.Web.Features.UserRoles
{
    public class UserRole : IdentityUserRole<int>
    {
        public virtual User User { get; set; }
        public virtual Role Role { get; set; }

        
    }

}
