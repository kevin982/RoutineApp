﻿using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string id, string token)
        {
            ViewBag.Errors = new List<string>();

            if (!string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(token))
            {
                token = token.Replace(' ', '+');

                var result = await _accountService.ConfirmEmailAsync(id, token);

                foreach (var error in result.Errors) ViewBag.Errors.Add(error.Description);

            }

            return View();
        }

        public IActionResult SignIn()
        {
            ViewBag.Succeded = null;
            return View();
        }

        [Route("Account/SignIn"), HttpPost]
        public async Task<IActionResult> SignIn(SignInModel model)
        {
            ViewBag.Errors = new List<string>(3);
            ViewBag.Succeded = null;

            if (ModelState.IsValid)
            {
                var result = await _accountService.SignIn(model);

                if (!result.Succeeded)
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
        public async Task<IActionResult> SignOut()
        {
            await _accountService.SignOut();
            return RedirectToAction("Index", "Home");
        }

    }
}
