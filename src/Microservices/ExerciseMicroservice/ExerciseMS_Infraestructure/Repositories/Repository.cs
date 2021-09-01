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
            await _context.Set<T>().AddAsync(data);
            return data;
        }

        public virtual async Task<T> DeleteAsync(Guid id)
        {
            T dataToEliminate = await GetByIdAsync(id);

            if (dataToEliminate is null) throw new ExerciseMSException("The entity to eliminate has not been found") { StatusCode = 400};

            _context.Set<T>().Remove(dataToEliminate);

            return dataToEliminate;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(int index = 0, int size = 0)
        {
            if (index == 0 && size == 0)
                return await _context.Set<T>()
                 .AsNoTrackingWithIdentityResolution()
                 .ToListAsync();

            return await _context
                .Set<T>()
                .AsNoTrackingWithIdentityResolution()
                .Skip(index * size)
                .Take(size)
                .ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            var result = await _context
                .Set<T>()
                .FindAsync(id);

            return result;
        }
    }
}
