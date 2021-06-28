using DomainRoutineApp.Mappers.Interfaces;
using DomainRoutineApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureRoutineApp.Mappers.Classes
{
    public class ImageMapper : IImageMapper
    {
        public List<string> MapDomainToStrings(List<Image> images)
        {
            List<string> result = new();

            images.ForEach(i => result.Add(i.Img));

            return result;
        }
    }
}
