using RoutineApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoutineApp.Mappers.Interfaces
{
    public interface IImageMapper
    {
        List<string> MapBitsToImages(List<Image> images);
    }
}
