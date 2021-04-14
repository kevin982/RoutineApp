using DomainRoutineApp.Mappers.Interfaces;
using DomainRoutineApp.Models.Entities;
using DomainRoutineApp.Models.Requests.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureRoutineApp.Mappers.Classes
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
                FirstName = model.FistName,
                LastName = model.LastName,
                UserName = model.Email
            };
        }
    }
}
