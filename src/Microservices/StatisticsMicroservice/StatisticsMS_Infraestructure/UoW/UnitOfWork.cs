using StatisticsMS_Core.Repositories;
using StatisticsMS_Core.UoW;
using StatisticsMS_Infraestructure.Data;
using StatisticsMS_Infraestructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatisticsMS_Infraestructure.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StatisticsMSContext _dbContext;

        public IStatisticsRepository Statistics { get; private set; }
        public UnitOfWork(StatisticsMSContext dbContext)
        {
            _dbContext = dbContext;

            Statistics = new StatisticsRepository(_dbContext);
        }

        public async Task CompleteAsync()
        {
            try
            {
                await _dbContext
                    .SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
