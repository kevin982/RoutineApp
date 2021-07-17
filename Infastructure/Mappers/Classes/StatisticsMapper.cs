using DomainRoutineApp.Mappers.Interfaces;
 
using DomainRoutineApp.Models.Requests.Statics;
using DomainRoutineLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureRoutineApp.Mappers.Classes
{
    public class StatisticsMapper : IStatisticsMapper
    {
        public UserWeight MapAddWeightToDomain(AddPersonWeightRequestModel model)
        {
            return new UserWeight
            {
                Kilos = model.Weight
            };
        }
    }
}
