using DomainRoutineLibrary.Entities;
using IdentityServer.Models;

namespace IdentityServer.Mapper
{
    public interface IAccountMapper
    {
        ApplicationUser MapSignUpRequestModelToDomain(SignUpRequestModel model);
    }
}