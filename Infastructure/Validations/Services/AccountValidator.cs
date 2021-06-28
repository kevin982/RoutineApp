using DomainRoutineApp.Models.Requests.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureRoutineApp.Validations.Services
{
    public class AccountValidator : IAccountValidator
    {
        public (bool Valid, string Message) ChangePasswordModelValidation(ChangePasswordRequestModel model)
        {
            if (model is null) return (false, "Model can not be null");

            if (string.IsNullOrEmpty(model.OldPassword) || string.IsNullOrEmpty(model.NewPassword) || string.IsNullOrEmpty(model.ConfirmNewPassword)) return (false, "The old password, the new password and the confirm new password are required");

            return (TheTwoPasswordsMatch(model.NewPassword, model.ConfirmNewPassword)) ? (true, "Ok") : (false, "The password and the confirm password must match");
        }

        public (bool Valid, string Message) ConfirmEmailModelValidation(ConfirmEmailRequestModel model)
        {
            if (model is null) return (false, "The model to confirm the email can not be null");

            if (string.IsNullOrEmpty(model.Token) || string.IsNullOrEmpty(model.UserId)) return (false, "The user id and token are required");

            if (model.Token.Contains(' ')) return (false, "The token can not have white spaces");

            return (true, "Ok");
        }

        public (bool Valid, string Message) CreateUserModelValidation(SignUpRequestModel model)
        {
            if (model is null) return (false, "The create user model can not be null");

            bool anyPropertyIsEmpty = string.IsNullOrEmpty(model.FirstName) || string.IsNullOrEmpty(model.LastName) || string.IsNullOrEmpty(model.Email) || model.Age == 0 || string.IsNullOrEmpty(model.Password) || string.IsNullOrEmpty(model.ConfirmPassword);

            if (anyPropertyIsEmpty) return (false, "All the Sign up model fields are required");

            if (!ValidEmail(model.Email)) return (false, "The email is not valid");

            if (model.Age < 18 || model.Age > 121) return (false, "The age must be in the range of 18 and 121");

            if (!TheTwoPasswordsMatch(model.Password, model.ConfirmPassword)) return (false, "The password and the confirm password must match");

            return (true, "Ok");
        }

        public (bool Valid, string Message) ResetPasswordModelValidation(ResetPasswordRequestModel model)
        {
            if (model is null) return (false, "The reset password model can not be null");

            bool empyFields = string.IsNullOrEmpty(model.Id) || string.IsNullOrEmpty(model.Token) || string.IsNullOrEmpty(model.NewPassword) || string.IsNullOrEmpty(model.ConfirmPassword);

            if (empyFields) return (false, "All the reset password model fields are required.");

            if (model.Token.Contains(' ')) return (false, "The token to reset the password must not have white spaces.");

            if (!TheTwoPasswordsMatch(model.NewPassword, model.ConfirmPassword)) return (false, "The password and the confirmation password must match");

            return (true, "Ok");
        }

        public (bool Valid, string Message) SendEmailToResetPasswordModelValidation(EmailResetPasswordRequestModel model)
        {
            if (model is null) return (false, "The model to send the email that resets the password can not be null");

            return (ValidEmail(model.UserEmail)) ? (true, "Ok") : (false, "The email to send the email that resets the password is not valid");
        }

        public (bool Valid, string Message) SignInModelValidation(SignInRequestModel model)
        {
            if (model is null) return (false, "The model to sign in can not be null.");

            if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password)) return (false, "The email and the password are required to sign in.");

            if (!ValidEmail(model.Email)) return (false, "The email to sign in is not valid.");

            return (true, "Ok");
        }

        public (bool Valid, string Message) SendEmailConfirmationModelValidation(SendEmailConfirmationRequestModel model)
        {
            if (model is null) return (false, "The model to send the email confirmation must not be null.");

            bool emptyFields = string.IsNullOrEmpty(model.Token) || string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.UserId) || string.IsNullOrEmpty(model.FirstName);

            if (emptyFields) return (false, "All the fields for the send email confirmation model are required.");

            if (ValidEmail(model.Email)) return (false, "The email to send the email confirmation is not valid");

            return (true, "Ok");
        }


        #region Helpers

        private bool TheTwoPasswordsMatch(string password1, string password2) => password1 == password2;

        private bool ValidEmail(string email)
        {
            try
            {
                MailAddress m = new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        #endregion
    }
}
