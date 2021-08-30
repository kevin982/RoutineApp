using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseMS_Core.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<bool> CreateAsync(T data);

        Task<bool> DeleteAsync(T data, Guid id, Guid? userId = null);

        Task<IEnumerable<T>> GetAllAsync(int index, int size);

        Task<T> GetByIdAsync(Guid id);
    }
}
