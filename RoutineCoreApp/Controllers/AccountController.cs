using DomainRoutineApp.Models.Requests.Account;
using DomainRoutineApp.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutineCoreApp.Controllers
{
    [Route("[controller]/[action]")]
    [AutoValidateAntiforgeryToken]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService = null;
        private readonly ILogger<AccountController> _logger = null;

        public AccountController(IAccountService accountService, ILogger<AccountController> logger)
        {
            _accountService = accountService;
            _logger = logger;
        }


        public IActionResult SignUp()
        {
            ViewBag.Errors = new List<string>();
            ViewBag.UserCreated = false;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpRequestModel model)
        {

            try
            {
                ViewBag.Errors = new List<string>();

                if (!ModelState.IsValid) throw new ArgumentException("The model is not valid");
                if (model is null) throw new ArgumentNullException("The model can not be null");

                var result = await _accountService.CreateUserAsync(model);

                if (!result.Succeeded)
                {
                    ViewBag.UserCreated = false;

                    foreach (var error in result.Errors)
                    {
                        ViewBag.Errors.Add(error.Description);
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An exception has been triggered.");
            }

            return View();

        }


        public async Task<IActionResult> ConfirmEmail(string id, string token)
        {

            try
            {
                ViewBag.Errors = new List<string>();

                if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(token))
                {
                    ViewBag.Errors.Add("Url is invalid.");
                    throw new ArgumentException("Url is invalid.");
                }


                token = token.Replace(' ', '+');

                var result = await _accountService.ConfirmEmailAsync(new ConfirmEmailRequestModel { UserId = id, Token = token });

                if (!result.Succeeded) throw new ArgumentException("Id or token invalid");

                foreach (var error in result.Errors) ViewBag.Errors.Add(error.Description);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error has been triggered.");
            }

            return View();
        }

        public IActionResult SignIn()
        {
            ViewBag.Succeded = null;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInRequestModel model)
        {
            ViewBag.Errors = new List<string>(3);
            ViewBag.Succeded = null;

            if (ModelState.IsValid)
            {
                var result = await _accountService.SignInAsync(model);

                if (result is null)
                {
                    ViewBag.Errors.Add("You have not created an account");
                    ViewBag.Succeded = false;

                }
                else if (!result.Succeeded)
                {
                    if (result.IsLockedOut) ViewBag.Errors.Add("You have entered invalid cretendials several times, so you are lockout for 1 hour.");
                    if (result.IsNotAllowed) ViewBag.Errors.Add("You are not allowed to signIn, you must confirm your email.");
                    if (result.IsLockedOut == false && result.IsNotAllowed == false) ViewBag.Errors.Add("Invalid credentials");
                    ViewBag.Succeded = false;
                }
                else
                {
                    ViewBag.Succeded = true;
                    return RedirectToAction("Index", "Home");
                }


            }

            return View();

        }


        [Authorize]
        public async Task<IActionResult> LogOut()
        {
            await _accountService.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult ChangePassword()
        {
            return View();
        }


        [HttpPost, Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequestModel model)
        {
            ViewBag.Errors = new List<string>();

            if (ModelState.IsValid)
            {
                var result = await _accountService.ChangePasswordAsync(model);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors) ViewBag.Errors.Add(error.Description);
                }

            }

            return View();
        }

        public IActionResult SendEmailToResetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendEmailToResetPassword(EmailResetPasswordRequestModel model)
        {
            ViewBag.MessageResult = "We have sent you an email!";

            if (ModelState.IsValid)
            {
                await _accountService.SendEmailToResetPasswordAsync(model);
            }

            return View();
        }


        public IActionResult ResetPassword(string id, string token)
        {
            ResetPasswordRequestModel model = new() { Id = id, Token = token };
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequestModel model)
        {
            ViewBag.Errors = new List<string>();

            if (ModelState.IsValid)
            {
                model.Token = model.Token.Replace(' ', '+');

                var result = await _accountService.ResetPasswordAsync(model);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors) ViewBag.Errors.Add(error.Description);
                }
            }

            return View();
        }

        public IActionResult AccessDenied()
        {

            return View();
        }

        public IActionResult ExternalLogin()
        {

            var auth = new AuthenticationProperties
            {
                RedirectUri = Url.Action(nameof(FacebookResponse))
            };

            return Challenge(auth, FacebookDefaults.AuthenticationScheme);
        }

        public async Task<IActionResult> FacebookResponse()
        {
            await _accountService.SignInFbAsync();

            return RedirectToAction("Index");
        }


    }
}
