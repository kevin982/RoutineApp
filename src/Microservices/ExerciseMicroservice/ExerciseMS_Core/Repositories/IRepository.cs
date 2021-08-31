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

        Task<bool> DeleteAsync(Guid id);

        Task<IEnumerable<T>> GetAllAsync(int index = 0, int size = 0);

        Task<T> GetByIdAsync(Guid id);
    }
}
