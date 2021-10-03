using System;
using System.Threading.Tasks;

namespace MVCRoutineAppClient.Services
{
    public interface IStatisticsService
    {
        Task<string> GetExerciseStatisticsAsync(string accessToken, Guid exerciseId, int month, int year);
    }
}