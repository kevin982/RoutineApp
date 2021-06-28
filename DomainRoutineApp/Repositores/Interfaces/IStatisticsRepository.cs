using DomainRoutineApp.Models.Entities;
using DomainRoutineApp.Models.Requests.Statics;
using DomainRoutineApp.Models.Responses.Statics;
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

        Task<List<Weight>> GetWeightStatisticsAsync(GetWeightStatisticsRequestModel model);

        Task<List<ExerciseDetail>> GetExerciseDetailsAsync(GetExerciseStatisticsRequestModel model);
    }
}
