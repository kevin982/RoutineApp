using DomainRoutineApp.Models.Requests.Account;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainRoutineApp.Services.Interfaces
{
    public interface IAccountService
    {
        Task<IdentityResult> CreateUserAsync(SignUpRequestModel model);

        Task<IdentityResult> ConfirmEmailAsync(ConfirmEmailRequestModel model);

        Task<SignInResult> SignInAsync(SignInRequestModel model);

        Task SignOutAsync();

        Task<IdentityResult> ChangePasswordAsync(ChangePasswordRequestModel model);

        Task SendEmailToResetPasswordAsync(EmailResetPasswordRequestModel model);

        Task<IdentityResult> ResetPasswordAsync(ResetPasswordRequestModel model);
    }
}
