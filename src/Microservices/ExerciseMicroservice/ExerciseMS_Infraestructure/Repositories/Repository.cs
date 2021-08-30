using ExerciseMS_Core.Repositories;
using ExerciseMS_Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
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

        public Repository(ExerciseMsDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateAsync(T data)
        {
            try
            {
               await _context.Set<T>().AddAsync(data);
               return true;
            }
            catch (Exception ex)
            {
                return false; 
            }
        }

        public async Task<bool> DeleteAsync(T data, Guid id)
        {
            try
            {
                _context.Set<T>().Remove(await GetByIdAsync(id));
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync(int index, int size)
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
                return null;
            }
        }

        public async Task<T> GetByIdAsync(Guid id)
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
                return null;
            }
        }
    }
}
