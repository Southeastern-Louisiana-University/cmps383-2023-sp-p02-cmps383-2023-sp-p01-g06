using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SP23.P02.Web.Features.Users;

namespace SP23.P02.Web.Controllers
{
    public class AuthenticationController : ControllerBase

    {
        private SignInManager<User> _signInManager;
        private UserManager<User> _userManager;
        public AuthenticationController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        //public void Login()
        //{
        //    var signInManager = scope.ServiceProvider.GetRequiredService<SignInManager<User>>();
        //    await signInManager.SignInAsync(new User
        //    {
        //        Username = "bob",
        //        Password = "Password123!"
        //    }, true);

        //    return;
        //}
    }
}
