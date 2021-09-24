using RoutineMS_Core.Models.Entities;
using System.Threading.Tasks;

namespace RoutineMS_Core.Repositories
{
    public interface ISetDetailRepository : IRepository<SetDetail>
    {
        Task DeleteOldDetailsAsync();
    }
}
