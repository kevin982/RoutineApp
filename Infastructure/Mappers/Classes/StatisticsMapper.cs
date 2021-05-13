using DomainRoutineApp.Mappers.Interfaces;
using DomainRoutineApp.Models.Entities;
using DomainRoutineApp.Models.Requests.Statics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureRoutineApp.Mappers.Classes
{
    public class StatisticsMapper : IStatisticsMapper
    {
        public Weight MapAddWeightToDomain(AddPersonWeightRequestModel model)
        {
            return new Weight
            {
                Kilos = model.Weight
            };
        }
    }
}
