using CategoryMS_Core.Exceptions;
using CategoryMS_Core.Repositories;
using CategoryMS_Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CategoryMS_Infraestructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected CategoryMsDbContext _context;

        public Repository(CategoryMsDbContext context)
        {
            _context = context;
        }

        public virtual async Task<T> CreateAsync(T data)
        {
            try
            {
                if (data is null) throw new CategoryMSException("The data to add can not be null") { StatusCode = 500 };
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
                List<T> result;

                result = await _context.Set<T>()
                 .AsNoTrackingWithIdentityResolution()
                 .ToListAsync();

                if (result is null) throw new CategoryMSException("There are not entities") { StatusCode = 404 };
                if (result.Count == 0) throw new CategoryMSException("There are not entities") { StatusCode = 404 };

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

                if (result is null) throw new CategoryMSException("There is not an entity with that id") { StatusCode = 404 };

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
