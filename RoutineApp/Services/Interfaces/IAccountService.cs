using Microsoft.AspNetCore.Identity;
using RoutineApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoutineApp.Services.Interfaces
{
    public interface IAccountService
    {
        Task<IdentityResult> CreateUserAsync(SignUpModel model);

        Task<IdentityResult> ConfirmEmailAsync(string id, string token);

        Task<SignInResult> SignIn(SignInModel model);
    }
}
