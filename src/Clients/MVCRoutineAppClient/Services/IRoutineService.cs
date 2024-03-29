﻿using MVCRoutineAppClient.Models;
using System;
using System.Threading.Tasks;

namespace MVCRoutineAppClient.Services
{
    public interface IRoutineService
    {
        Task<string> AddExerciseToRoutineAsync(string accessToken, AddExerciseToRoutineRequest request);
        Task<string> RemoveExerciseFromRoutineAsync(string accessToken, Guid exerciseId);
        Task<string> GetExerciseToDoAsync(string accessToken);
        Task<string> PostSetDoneAsync(ExerciseDoneRequestModel model, string accessToken);
    }
}