using MediatR;
using StatisticsMS_Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatisticsMS_Application.Queries
{
    public class GetExerciseStatisticsQuery : IRequest<IEnumerable<DtoStatistic>>
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public Guid ExerciseId { get; set; }

        public GetExerciseStatisticsQuery(Guid exerciseId, int month, int year)
        {
            ExerciseId = exerciseId;
            Month = month;
            Year = year;
        }
    }
}
