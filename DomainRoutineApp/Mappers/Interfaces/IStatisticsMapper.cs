using DomainRoutineApp.Models.Entities;
using DomainRoutineApp.Models.Requests.Statics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainRoutineApp.Mappers.Interfaces
{
    public interface IStatisticsMapper
    {
        Weight MapAddWeightToDomain(AddPersonWeightRequestModel model);
    }
}
