using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutineMS_Core.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<T> CreateAsync(T data);

        Task<T> DeleteAsync(Guid id);

        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetByIdAsync(Guid id);
    }
}
