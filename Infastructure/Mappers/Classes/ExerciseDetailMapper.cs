using DomainRoutineApp.Mappers.Interfaces;
 
using DomainRoutineApp.Models.Requests.Exercise;
using DomainRoutineLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureRoutineApp.Mappers.Classes
{
    public class ExerciseDetailMapper : IExerciseDetailMapper
    {
        public ExerciseSetDetail MapExerciseDoneRequestToDomain(ExerciseDoneRequestModel model)
        {
            return new ExerciseSetDetail
            {
                ExerciseId = model.ExerciseId,
                DayDone = DateTime.Now,
                Repetitions = model.Repetitions,
                Weight = model.Weight
            };
        }
    }
}
