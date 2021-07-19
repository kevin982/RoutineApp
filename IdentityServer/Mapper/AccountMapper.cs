using DomainRoutineLibrary.Entities;
using IdentityServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.Mapper
{
    public class AccountMapper : IAccountMapper
    {
        public User MapSignUpRequestModelToDomain(SignUpRequestModel model)
        {
            if (model is null) return null;

            return new User
            {
                Email = model.Email,
                Age = model.Age,
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.Email,
                CreatedOn = DateTime.UtcNow
            };
        }
    }
}
