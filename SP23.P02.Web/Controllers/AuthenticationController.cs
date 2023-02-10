using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SP23.P02.Web.Data;
using SP23.P02.Web.Features.Login;
using SP23.P02.Web.Features.Users;
using System.Reflection.Metadata;
using System.Security.Claims;

namespace SP23.P02.Web.Controllers
{
    [Route("/api/authentication/")]
    public class AuthenticationController : ControllerBase

    {
        private SignInManager<User> _signInManager;
        private UserManager<User> _userManager;
        private readonly DataContext _dataContext;
        public AuthenticationController(UserManager<User> userManager, SignInManager<User> signInManager, DataContext dataContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dataContext = dataContext;
        }

        [HttpPost]
        [Route("/loginTest")]
        public async Task<ActionResult> LoginTest()
        {
            var user = _dataContext.Users.First(x => x.UserName == "bob");
            await _signInManager.SignInAsync(user, true);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto user)
        {
            var userFound = _userManager.Users.First(x => x.UserName == user.UserName);
            var passwordIsValid = await _userManager.CheckPasswordAsync(userFound, user.Password);

            if (passwordIsValid)
            {
                await _signInManager.SignInAsync(userFound, false);
                //await _signInManager.CheckPasswordSignInAsync(userFound, user.Password, false);
                return Ok(_dataContext.Users.Select(x => new UserDto
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    Roles = x.Roles
                }));
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpGet("me")]
        public async Task<ActionResult<UserDto>> Me()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);



            if (user != null)
            {
                return Ok(new UserDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Roles = user.Roles
                });
            }
            else
            {
                return BadRequest();
            }
        }



    }
}
