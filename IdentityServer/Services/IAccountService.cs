using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace IdentityServer.Services
{
    public interface IAccountService
    {
        //Task<IdentityResult> ChangePasswordAsync(ChangePasswordRequestModel model);
        Task<IdentityResult> ConfirmEmailAsync(string token, string userId);
        Task<IdentityResult> CreateUserAsync(SignUpRequestModel model);
        //Task<IdentityResult> ResetPasswordAsync(ResetPasswordRequestModel model);
        //Task SendEmailToResetPasswordAsync(EmailResetPasswordRequestModel model);
        //Task<SignInResult> SignInAsync(SignInRequestModel model);
        //Task SignOutAsync();
    }
}