using Microsoft.AspNetCore.Mvc;
using RoutineApp.Models;
using RoutineApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoutineApp.Controllers
{

    
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService = null;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }



        [Route("Account/SignUp")]
        public IActionResult SignUp()
        {
            ViewBag.Errors = new List<string>();
            ViewBag.UserCreated = false;
            return View();
        }

        [Route("Account/SignUp"), HttpPost]
        public async Task<IActionResult> SignUp(SignUpModel model)
        {

            ViewBag.Errors = new List<string>();

            if (ModelState.IsValid)
            {
                var result = await _accountService.CreateUserAsync(model);

                if (!result.Succeeded)
                {
                    ViewBag.UserCreated = false;

                    foreach (var error in result.Errors)
                    {
                        ViewBag.Errors.Add(error.Description);
                    }
                }
                else
                {
                    ViewBag.UserCreated = true;
                }

            }



            return View();
        }




    }
}
