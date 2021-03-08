using Microsoft.AspNetCore.Identity;
using RoutineApp.Data.Entities;
using RoutineApp.Models;
using RoutineApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoutineApp.Services.Classes
{
    public class AccountService:IAccountService
    {
        private readonly UserManager<User> _userManager = null;

        public AccountService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> CreateUserAsync(SignUpModel model)
        {

            User usuario = model.Clone() as User;
            usuario.UserName = model.Email;

            var result = await _userManager.CreateAsync(usuario, model.Password);

            return result;
        }


    }
}
