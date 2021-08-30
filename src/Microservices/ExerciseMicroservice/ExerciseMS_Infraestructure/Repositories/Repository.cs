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

        public virtual async Task<bool> CreateAsync(T data)
        {
            try
            {
               await _context.Set<T>().AddAsync(data);
               return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while adding an entity because of {ex.Message}");
                return false; 
            }
        }

        public virtual async Task<bool> DeleteAsync(T data, Guid id, Guid? userId = null)
        {
            try
            {
                _context.Set<T>().Remove(await GetByIdAsync(id));
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while deleting an entity because of {ex.Message}");
                return false;
            }
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(int index, int size)
        {
            try
            {
                var result = await _context
                    .Set<T>()
                    .AsNoTrackingWithIdentityResolution()
                    .Skip(index * size)
                    .Take(size)
                    .ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting all entities because of {ex.Message}");
                return null;
            }
        }

        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            try
            {
                var result = await _context
                    .Set<T>()
                    .FindAsync(id);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting an entity because of {ex.Message}");
                return null;
            }
        }
    }
}
