using DomainRoutineApp.Models.Requests.Day;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureRoutineApp.Validations.Services
{
    public class DayServiceValidator : IDayServiceValidator
    {
        public (bool Valid, string Message) GetDayByIdModelValidation(GetDayRequestModel model)
        {
            if (model is null) return (false, "The model to get the day must not be null.");

            if (model.DayId < 1 || model.DayId > 7) return (false, "The day id must be between 1 and 7.");

            return (true, "Ok");
        }
    }
}
