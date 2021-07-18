using DomainRoutineApp.Mappers.Interfaces;
 
using DomainRoutineApp.Models.Requests.Statics;
using DomainRoutineApp.Models.Responses.Statics;
using DomainRoutineApp.Repositores.Interfaces;
using DomainRoutineLibrary;
using DomainRoutineLibrary.Entities;
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
        //private readonly UserManager<User> _userManager = null;
        private readonly IStatisticsMapper _statisticsMapper = null;
        


        public StatisticsRepository(RoutineContext context, IStatisticsMapper statisticsMapper)
        {
            _context = context;
            //_userManager = userManager;
            _statisticsMapper = statisticsMapper;
        }
        

        public async Task AddWeightAsync(AddPersonWeightRequestModel model)
        {
            //var user = await _userManager.FindByIdAsync(model.UserId);

            UserWeight weight = _statisticsMapper.MapAddWeightToDomain(model);

            weight.Date = DateTime.Now;

            //user.UserWeights.Add(weight);

            await _context.SaveChangesAsync();
        }

        public async Task<List<ExerciseSetDetail>> GetExerciseDetailsAsync(GetExerciseStatisticsRequestModel model)
        {
            return await _context.ExerciseSetDetails
                .AsNoTrackingWithIdentityResolution()
                .Where(e => e.ExerciseId == model.ExerciseId)
                .ToListAsync();
        }

        public async Task<List<UserWeight>> GetWeightStatisticsAsync(GetWeightStatisticsRequestModel model)
        {
            var weights = await _context.UserWeights
                .AsNoTrackingWithIdentityResolution()
                .Where(w => w.UserId == model.UserId)
                .ToListAsync();


            return weights;
        }

    }
}
