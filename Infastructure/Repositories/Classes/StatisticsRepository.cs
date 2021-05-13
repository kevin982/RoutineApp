using DomainRoutineApp.Mappers.Interfaces;
using DomainRoutineApp.Models.Entities;
using DomainRoutineApp.Models.Requests.Statics;
using DomainRoutineApp.Models.Responses.Statics;
using DomainRoutineApp.Repositores.Interfaces;
using InfrastructureRoutineApp.Services.Classes;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureRoutineApp.Repositories.Classes
{
    public class StatisticsRepository : IStatisticsRepository
    {
        private readonly RoutineContext _context = null;
        private readonly UserManager<User> _userManager = null;
        private readonly IStatisticsMapper _statisticsMapper = null;
        


        public StatisticsRepository(RoutineContext context, UserManager<User> userManager, IStatisticsMapper statisticsMapper)
        {
            _context = context;
            _userManager = userManager;
            _statisticsMapper = statisticsMapper;
        }
        

        public async Task AddWeightAsync(AddPersonWeightRequestModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);

            Weight weight = _statisticsMapper.MapAddWeightToDomain(model);

            weight.Date = DateTime.Now;

            user.Weights.Add(weight);

            await _context.SaveChangesAsync();
        }

        public async Task<List<Weight>> GetWeightStatisticsAsync(GetWeightStatisticsRequestModel model)
        {
            var weights = await _context.Weights
                .Where(w => w.UserId == model.UserId)
                .ToListAsync();


            return weights;
        }

    }
}
