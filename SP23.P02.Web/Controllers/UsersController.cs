using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SP23.P02.Web.Data;
using SP23.P02.Web.Features.Roles;
using SP23.P02.Web.Features.Users;
using System.Reflection.Metadata;

namespace SP23.P02.Web.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize (Roles = "Admin")]
    public class UsersController : ControllerBase
    {
        private readonly DbSet<User> users;
        private readonly DataContext dataContext;
        private SignInManager<User> _signInManager;
        private UserManager<User> _userManager;

        public UsersController(UserManager<User> userManager, SignInManager<User> signInManager, DataContext dataContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            this.dataContext = dataContext;
            users = dataContext.Set<User>();
        }

        [HttpGet]
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

        [HttpPost]
        public async Task<ActionResult<UserCreateDto>> CreateUserAsync(UserCreateDto userCreateDto)
        {
            if (userCreateDto == null)
            {
                return BadRequest();
            }
            if(userCreateDto.UserName.IsNullOrEmpty()) 
            {
                return BadRequest();
            }
            if (userCreateDto.Password.IsNullOrEmpty())
            {
                return BadRequest();
            }
            if(userCreateDto.UserName == dataContext.Users.First().UserName)
            {
                return BadRequest();
            }
            if(userCreateDto.Roles.IsNullOrEmpty())
            {
                return BadRequest();
            }

            var userToCreate = new User
            {
                UserName = userCreateDto.UserName,
            };

            await _userManager.CreateAsync(userToCreate
            , "Password123!");

            var temp = dataContext.Users.First(x => x.UserName == userToCreate.UserName);
            await _userManager.AddToRoleAsync(temp, "User");


            var rolesList = await _userManager.GetRolesAsync(userToCreate);

            var userToReturn = new UserDto
            {
                Id = userToCreate.Id,
                UserName = userCreateDto.UserName,
                Roles = rolesList
            };

            return Ok(userToReturn);
        }




        private static IQueryable<UserDto> GetUserDtos(IQueryable<User> users)
        {
            return users
                .Select(x => new UserDto
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    Roles = x.Roles.Select(y => y.Role!.Name).ToArray()
                });
        }
    }
    }
