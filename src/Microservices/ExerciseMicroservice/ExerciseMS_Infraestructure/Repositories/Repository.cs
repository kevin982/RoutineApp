using ExerciseMS_Core.Exceptions;
using ExerciseMS_Core.Repositories;
using ExerciseMS_Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseMS_Infraestructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected ExerciseMsDbContext _context;
        protected ILogger _logger;

        public Repository(ExerciseMsDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        public virtual async Task<T> CreateAsync(T data)
        {
            try
            {
                if (data is null) throw new ExerciseMSException("The data to add can not be null") { StatusCode = 500};
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
                if (id == Guid.Empty) throw new ExerciseMSException("The id for the entity you want to delete can not be null") { StatusCode = 500};

                T dataToEliminate = await GetByIdAsync(id);

                _context.Set<T>().Remove(dataToEliminate);

                return dataToEliminate;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(int index = 0, int size = 0)
        {
            try
            {
                List<T> result;

                if (index == 0 && size == 0)
                    result = await _context.Set<T>()
                     .AsNoTrackingWithIdentityResolution()
                     .ToListAsync();

                result =  await _context
                    .Set<T>()
                    .AsNoTrackingWithIdentityResolution()
                    .Skip(index * size)
                    .Take(size)
                    .ToListAsync();

                if (result is null) throw new ExerciseMSException("There are not entities") { StatusCode = 404 };
                if (result.Count == 0) throw new ExerciseMSException("There are not entities") { StatusCode = 404 };

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

                if (result is null) throw new ExerciseMSException("There is not an entity with that id") {StatusCode = 404};

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
