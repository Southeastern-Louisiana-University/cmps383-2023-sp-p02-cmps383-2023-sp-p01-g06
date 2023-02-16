using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SP23.P02.Web.Data;
using SP23.P02.Web.Features.Roles;

namespace SP23.P02.Web.Controllers
{
    [Route("api/roles")]
    [ApiController]
    public class RolesController : ControllerBase
    {

        //private readonly DbSet<Role> roles;
        //private readonly DataContext dataContext;

        //public RolesController(DataContext dataContext)
        //{
        //    this.dataContext = dataContext;
        //    roles = dataContext.Set<Role>();
        //}

        //[HttpGet]
        ////[Authorize (Roles = "Admin")]
        //public IQueryable<RoleDto> GetAllRoles()
        //{   
        //    return GetRoleDtos(roles);
        //}

        //[HttpGet]
        //[Route("{id}")]
        //public ActionResult<RoleDto> GetRoleById(int id)
        //{
        //    var result = GetRoleDtos(roles.Where(x => x.Id == id)).FirstOrDefault();
        //    if (result == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(result);
        //}




        //    private static IQueryable<RoleDto> GetRoleDtos(IQueryable<Role> roles)
        //    {
        //        return roles
        //            .Select(x => new RoleDto
        //            {
        //                Id = x.Id,
        //                Name = x.Name
        //                //Add list of users here
        //            });
        //    }
    }

}
