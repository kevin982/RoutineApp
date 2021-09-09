using IdentityMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityMicroservice.Mappers
{
    public class AccountMapper : IAccountMapper
    {
        public ApplicationUser MapSignUpRequestModelToDomain(SignUpRequestModel model)
        {
            try
            {
                return new ApplicationUser
                {
                    UserName = model.UserName,
                    Email = model.Email
                };
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
