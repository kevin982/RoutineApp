using DomainRoutineApp.Models.Requests.Statics;
using DomainRoutineApp.Models.Responses.Statics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainRoutineApp.Services.Interfaces
{
    public interface IStatisticsService
    {
        Task AddWeightAsync(AddPersonWeightRequestModel model);

        Task<WeightStatisticsResponseModel> GetWeightStatisticsAsync();
    }
}
