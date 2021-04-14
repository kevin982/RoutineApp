using DomainRoutineApp.Models.Entities;
using DomainRoutineApp.Models.Requests.Day;
using DomainRoutineApp.Repositores.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureRoutineApp.Repositories.Classes
{
    public class DayRepository : IDayRepository
    {

        private readonly RoutineContext _context = null;

        public DayRepository(RoutineContext context)
        {
            _context = context;
        }

        public async Task<List<Day>> GetAllDaysAsync()
        {
            return await _context.Days.AsNoTracking().ToListAsync();
        }

        public async Task<Day> GetDayByIdAsync(GetDayRequestModel model)
        {
            return await _context.Days
                .Where(d => d.Id == model.DayId)
                .FirstOrDefaultAsync();
        }

        public async Task<int> GetDayIdAsync()
        {
            string day = DateTime.Now.DayOfWeek.ToString();

            var result = await _context.Days
                .AsNoTracking()
                .Where(d => d.DayName == day)
                .Select(d => new { d.Id })
                .SingleAsync();

            return result.Id;
        }
    }
}
