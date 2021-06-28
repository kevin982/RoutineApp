using Castle.Core.Logging;
using DomainRoutineApp.Models.Requests.Account;
using DomainRoutineApp.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using RoutineCoreApp.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Controllers
{
    public class AccountControllerShould
    {
        private  AccountController _accountController;
        
        private readonly Mock<IAccountService> _accountService = new Mock<IAccountService>();
        
        private readonly Mock<ILogger<AccountController>> _logger = new Mock<ILogger<AccountController>>();
    
 

        

    }
}
