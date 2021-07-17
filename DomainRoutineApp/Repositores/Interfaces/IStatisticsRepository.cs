
using DomainRoutineApp.Models.Requests.Statics;
using DomainRoutineApp.Models.Responses.Statics;
using DomainRoutineLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainRoutineApp.Repositores.Interfaces
{
    public interface IStatisticsRepository
    {
        Task AddWeightAsync(AddPersonWeightRequestModel model);

        Task<List<UserWeight>> GetWeightStatisticsAsync(GetWeightStatisticsRequestModel model);

        Task<List<ExerciseSetDetail>> GetExerciseDetailsAsync(GetExerciseStatisticsRequestModel model);
    }
}
