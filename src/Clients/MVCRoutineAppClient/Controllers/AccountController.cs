using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCRoutineAppClient.Controllers
{
    [Authorize]
    [ApiController]
    public class AccountController : Controller
    {
        [Route("/v1/Account/Logout")]
        public IActionResult Logout()
        {
            return SignOut("Cookies", "oidc");
        }

        [Route("/v1/Account/SignIn")]
        public IActionResult SignIn()
        {
            return Redirect("https://localhost:9000");
        }

        [AllowAnonymous]
        [Route("/v1/Account/SignUp")]
        public IActionResult SignUp()
        {
            return RedirectPermanent("https://localhost:9002/Account/SignUp");
        }

        [AllowAnonymous]
        [Route("/v1/Account/ForgetPassword")]
        public IActionResult ForgetPassword()
        {
            return RedirectPermanent("https://localhost:9002/Account/SendEmailToResetPassword");
        }

        [Route("/v1/Account/ChangePassword")]
        public IActionResult ChangePassword()
        {
            return RedirectPermanent("https://localhost:9002/Account/ChangePassword");
        }
    }
}
