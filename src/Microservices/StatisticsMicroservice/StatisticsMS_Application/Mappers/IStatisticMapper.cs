using StatisticsMS_Core.Dtos;
using StatisticsMS_Core.Models.Entities;
using System.Collections.Generic;

namespace StatisticsMS_Application.Mappers
{
    public interface IStatisticMapper
    {
        IEnumerable<DtoStatistic> MapEntityToDto(IEnumerable<Statistic> statistics);
        DtoStatistic MapEntityToDto(Statistic statistic);
    }
}