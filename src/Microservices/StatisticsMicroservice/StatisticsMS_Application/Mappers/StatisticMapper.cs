using StatisticsMS_Core.Dtos;
using StatisticsMS_Core.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatisticsMS_Application.Mappers
{
    public class StatisticMapper : IStatisticMapper
    {
        public IEnumerable<DtoStatistic> MapEntityToDto(IEnumerable<Statistic> statistics)
        {
            List<DtoStatistic> result = new();

            foreach (var statistic in statistics) result.Add(MapEntityToDto(statistic));

            return result;
        }
        public DtoStatistic MapEntityToDto(Statistic statistic)
        {
            return new DtoStatistic()
            {
                Date = $"{statistic.DayDone.Month}/{statistic.DayDone.Day}/{statistic.DayDone.Year}",
                Weight = statistic.Weight,
                Repetitions = statistic.Repetitions
            };
        }
    }
}
