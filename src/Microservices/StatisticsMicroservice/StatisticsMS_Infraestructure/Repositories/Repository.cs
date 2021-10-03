using Microsoft.EntityFrameworkCore;
using StatisticsMS_Core.Exceptions;
using StatisticsMS_Core.Repositories;
using StatisticsMS_Infraestructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatisticsMS_Infraestructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected StatisticsMSContext _context;

        public Repository(StatisticsMSContext context)
        {
            _context = context;
        }

        public virtual async Task<T> CreateAsync(T data)
        {
            try
            {
                if (data is null) throw new StatisticsMSException("The data to add can not be null") { StatusCode = 500 };
                await _context.Set<T>().AddAsync(data);
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }
  
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                List<T> result = await _context.Set<T>()
                     .AsNoTrackingWithIdentityResolution()
                     .ToListAsync();

                if (result is null || result?.Count == 0) throw new StatisticsMSException("There are not entities") { StatusCode = 404 };

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
 
    }
    
}
