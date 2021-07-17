
using DomainRoutineApp.Models.Requests.Statics;
using DomainRoutineLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainRoutineApp.Mappers.Interfaces
{
    public interface IStatisticsMapper
    {
        UserWeight MapAddWeightToDomain(AddPersonWeightRequestModel model);
    }
}
