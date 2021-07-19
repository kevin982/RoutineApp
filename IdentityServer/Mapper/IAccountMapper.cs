using DomainRoutineLibrary.Entities;
using IdentityServer.Models;

namespace IdentityServer.Mapper
{
    public interface IAccountMapper
    {
        User MapSignUpRequestModelToDomain(SignUpRequestModel model);
    }
}