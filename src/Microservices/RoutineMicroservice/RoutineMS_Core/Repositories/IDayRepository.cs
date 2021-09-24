using RoutineMS_Core.Models.Entities;
using System.Threading.Tasks;

namespace RoutineMS_Core.Repositories
{
    public interface IDayRepository : IRepository<Day>
    {
        Task<Day> GetDayByDayNumberAsync(int dayNumber);
    }
}
