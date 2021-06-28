using DomainRoutineApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainRoutineApp.Mappers.Interfaces
{
    public interface IImageMapper
    {
        List<string> MapDomainToStrings(List<Image> images);
    }
}
