using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SP23.P02.Web.Data;
using SP23.P02.Web.Features.Roles;
using SP23.P02.Web.Features.Users;

namespace SP23.P02.Web.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DbSet<User> users;
        private readonly DataContext dataContext;

        public UsersController(DataContext dataContext)
        {
            this.dataContext = dataContext;
            users = dataContext.Set<User>();
        }

        [HttpGet]
        //[Authorize (Roles = "Admin")]
        public IQueryable<UserDto> GetAllRoles()
        {
            return GetUserDtos(users);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<UserDto> GetUserById(int id)
        {
            var result = GetUserDtos(users.Where(x => x.Id == id)).FirstOrDefault();
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }




        private static IQueryable<UserDto> GetUserDtos(IQueryable<User> users)
        {
            return users
                .Select(x => new UserDto
                {
                    Id = x.Id,
                    UserName = x.UserName
                    //Add list of users here
                });
        }
    }
}
