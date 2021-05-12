using DomainRoutineApp.Mappers.Interfaces;
using DomainRoutineApp.Models.Entities;
using DomainRoutineApp.Models.Requests.Exercise;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureRoutineApp.Mappers.Classes
{
    public class ExerciseDetailMapper : IExerciseDetailMapper
    {
        public ExerciseDetail MapExerciseDoneRequestToDomain(ExerciseDoneRequestModel model)
        {
            return new ExerciseDetail
            {
                ExerciseId = model.ExerciseId,
                DayDone = DateTime.Now,
                Repetitions = model.Repetitions,
                Weight = model.Weight
            };
        }
    }
}
