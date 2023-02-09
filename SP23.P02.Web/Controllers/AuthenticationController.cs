using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SP23.P02.Web.Data;
using SP23.P02.Web.Features.Login;
using SP23.P02.Web.Features.Users;
using System.Reflection.Metadata;

namespace SP23.P02.Web.Controllers
{
    [Route("api/authentication")]
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
        [Route("/login")]
        public async Task<ActionResult> Login()
        {
            var user = _dataContext.Users.First(x => x.UserName == "bob");
            await _signInManager.SignInAsync(user, true);
            return Ok();
        }

        //[Route("login")]
        //public UserDto Login(LoginDto login)
        //{

        //    return new UserDto
        //    {
        //        UserName = login.UserName,
        //        Password = login.Password
        //    };
        //    //Returns UserDto
        //}
    }
}
