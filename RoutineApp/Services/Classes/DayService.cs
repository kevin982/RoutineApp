using Microsoft.EntityFrameworkCore;
using RoutineApp.Data;
using RoutineApp.Data.Entities;
using RoutineApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoutineApp.Services.Classes
{
    public class DayService : IDayService
    {
        private readonly RoutineContext _context = null;

        public DayService(RoutineContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Day>> GetAllDaysAsync()
        {
            return await _context.Days.AsNoTracking().ToListAsync();
        }

    }
}
