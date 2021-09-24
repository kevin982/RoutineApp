using Microsoft.EntityFrameworkCore;
using RoutineMS_Core.Exceptions;
using RoutineMS_Core.Repositories;
using RoutineMS_Infraestructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoutineMS_Infraestructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected RoutineMsDbContext _context;

        public Repository(RoutineMsDbContext context)
        {
            _context = context;
        }

        public virtual async Task<T> CreateAsync(T data)
        {
            try
            {
                if (data is null) throw new RoutineMSException("The data to add can not be null") { StatusCode = 500 };
                await _context.Set<T>().AddAsync(data);
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual async Task<T> DeleteAsync(Guid id)
        {
            try
            {
                if (id == Guid.Empty) throw new RoutineMSException("The id for the entity you want to delete can not be null") { StatusCode = 500 };

                T dataToEliminate = await GetByIdAsync(id);

                _context.Set<T>().Remove(dataToEliminate);

                return dataToEliminate;
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
                 
                if (result is null || result?.Count == 0) throw new RoutineMSException("There are not entities") { StatusCode = 404 };
                
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            try
            {
                var result = await _context
                    .Set<T>()
                    .FindAsync(id);

                if (result is null) throw new RoutineMSException("There is not an entity with that id") { StatusCode = 404 };

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
