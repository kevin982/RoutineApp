using DomainRoutineApp.Models.Requests.Day;

namespace InfrastructureRoutineApp.Validations.Services
{
    public interface IDayServiceValidator
    {
        (bool Valid, string Message) GetDayByIdModelValidation(GetDayRequestModel model);
    }
}