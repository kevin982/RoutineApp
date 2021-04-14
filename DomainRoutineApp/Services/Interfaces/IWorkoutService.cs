using DomainRoutineApp.Models.Responses.Exercise;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainRoutineApp.Services.Interfaces
{
    public interface IWorkoutService
    {
        Task<ExerciseWorkOutResponseModel> GetNextExerciseAsync();
    }
}
