using DomainRoutineApp.Models.Requests.Account;

namespace InfrastructureRoutineApp.Validations.Services
{
    public interface IAccountValidator
    {
        (bool Valid, string Message) ChangePasswordModelValidation(ChangePasswordRequestModel model);
        (bool Valid, string Message) ConfirmEmailModelValidation(ConfirmEmailRequestModel model);
        (bool Valid, string Message) CreateUserModelValidation(SignUpRequestModel model);
        (bool Valid, string Message) ResetPasswordModelValidation(ResetPasswordRequestModel model);
        (bool Valid, string Message) SendEmailConfirmationModelValidation(SendEmailConfirmationRequestModel model);
        (bool Valid, string Message) SendEmailToResetPasswordModelValidation(EmailResetPasswordRequestModel model);
        (bool Valid, string Message) SignInModelValidation(SignInRequestModel model);
    }
}