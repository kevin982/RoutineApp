using StatisticsMS_Core.Models.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StatisticsMS_Core.Repositories
{
    public interface IStatisticsRepository : IRepository<Statistic>
    {
        Task<IEnumerable<Statistic>> GetExerciseStatisticsAsync(Guid userId, Guid exerciseId, int year, int month);
    }
}
