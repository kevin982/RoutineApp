using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatisticsMS_Core.Repositories
{
    public interface IRepository<T>
    {
        Task<T> CreateAsync(T data);

        Task<IEnumerable<T>> GetAllAsync();
    }
}
