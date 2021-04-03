using RoutineApp.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoutineApp.Services.Interfaces
{
    public interface IDayService
    {
        Task<IEnumerable<Day>> GetAllDaysAsync();
    }
}