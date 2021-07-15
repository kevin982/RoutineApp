using DomainRoutineApp.Mappers.Interfaces;
using DomainRoutineApp.Models.Entities;
using DomainRoutineApp.Models.Requests.Account;
using DomainRoutineApp.Services.Interfaces;
using InfrastructureRoutineApp.Services.Classes;
using InfrastructureRoutineApp.Validations.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Services
{
    public class AccountServiceShould
    {
        private readonly Mock<UserManager<User>> _userManager = new Mock<UserManager<User>>();
        private readonly Mock<SignInManager<User>> _signInManager = new Mock<SignInManager<User>>();
        private readonly Mock<IEmailService> _emailService = new Mock<IEmailService>();
        private readonly Mock<IUserService> _userService = new Mock<IUserService>();
        private readonly Mock<IAccountMapper> _accountMapper = new Mock<IAccountMapper>();
        private readonly Mock<IAccountValidator> _accountValidator = new Mock<IAccountValidator>();
        private readonly Mock<ILogger<AccountService>> _logger = new Mock<ILogger<AccountService>>();

        private AccountService accountService;

        #region ChangePasswordMethod

        [Fact]
        public async Task ThrowExceptionIfChangePasswordModelIsNotValid()
        {
            //Arrange
            ChangePasswordRequestModel model = null;

            _accountValidator.Setup(v => v.ChangePasswordModelValidation(model)).Returns((false, "Error"));

            

            //Act

            accountService = new (_userManager.Object, _signInManager.Object, _emailService.Object, _userService.Object, _accountMapper.Object, _accountValidator.Object, _logger.Object);

            //Assert

            await Assert.ThrowsAsync<Exception>(async () =>
            {
                await accountService.ChangePasswordAsync(model);
            });

        }


        #endregion
 
    }
}
