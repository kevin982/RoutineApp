using RoutineMS_Core.Models.Entities;
using RoutineMS_Core.Repositories;
using RoutineMS_Infraestructure.Data;
using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace RoutineMS_Infraestructure.Repositories
{
    public class SetDetailRepository : Repository<SetDetail>, ISetDetailRepository
    {
        public SetDetailRepository(RoutineMsDbContext context) : base(context) { }

        public async Task DeleteOldDetailsAsync()
        {
            try
            {
                DateTime today = DateTime.UtcNow;

                var detailsToRemove = await
                    _context
                    .SetsDetails
                    .Where(d => (d.Date.Year != today.Year) || (d.Date.Month != today.Month) || (d.Date.Day != today.Day))
                    .ToListAsync();

                if (detailsToRemove is null || detailsToRemove?.Count == 0) return;

                _context.RemoveRange(detailsToRemove);
            }
            catch (Exception)
            {
                throw;
            }

        }

    }
}
