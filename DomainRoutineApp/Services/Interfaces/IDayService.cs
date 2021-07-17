 
using DomainRoutineApp.Models.Requests.Day;
using DomainRoutineLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainRoutineApp.Services.Interfaces
{
    public interface IDayService
    {
        Task<List<Day>> GetAllDaysAsync();

        Task<int> GetDayIdAsync();

        Task<Day> GetDayByIdAsync(GetDayRequestModel model);
    }
}
