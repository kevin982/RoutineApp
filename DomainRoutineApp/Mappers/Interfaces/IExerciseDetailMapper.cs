﻿ 
using DomainRoutineApp.Models.Requests.Exercise;
using DomainRoutineLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainRoutineApp.Mappers.Interfaces
{
    public interface IExerciseDetailMapper
    {
        ExerciseSetDetail MapExerciseDoneRequestToDomain(ExerciseDoneRequestModel model);
    }
}
