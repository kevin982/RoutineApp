using Microsoft.EntityFrameworkCore;
using StatisticsMS_Core.Exceptions;
using StatisticsMS_Core.Models.Entities;
using StatisticsMS_Core.Repositories;
using StatisticsMS_Infraestructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatisticsMS_Infraestructure.Repositories
{
    public class StatisticsRepository : Repository<Statistic>, IStatisticsRepository
    {
        public StatisticsRepository(StatisticsMSContext context) : base(context) { }

        public async Task<IEnumerable<Statistic>> GetExerciseStatisticsAsync(Guid userId, Guid exerciseId, int year, int month)
        {
            try
            {
                var statistics = await _context
                    .Statistics
                    .AsNoTracking()
                    .Where(s => (s.UserId == userId) && (s.ExerciseId == exerciseId))
                    .OrderBy(s => s.DayDone)
                    .ToListAsync();

                if (statistics is null || statistics.Count == 0) throw new StatisticsMSException($"There are no statistics for the exercise") { StatusCode = 404 };

                if(year <= 0 || month <= 0) return statistics;

                List<Statistic> result = new();

                foreach (Statistic statistic in statistics)
                {
                    if(statistic.DayDone.Year == year && statistic.DayDone.Month == month) result.Add(statistic);   
                }

                if(result.Count == 0) throw new StatisticsMSException($"There are no statistics for the exercise in the month {month}, year {year}") { StatusCode = 404 };

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
 
    }
}
