using DomainRoutineApp.Models.Entities;
using DomainRoutineApp.Models.Requests.Exercise;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainRoutineApp.Mappers.Interfaces
{
    public interface IExerciseDetailMapper
    {
        ExerciseDetail MapExerciseDoneRequestToDomain(ExerciseDoneRequestModel model);
    }
}
