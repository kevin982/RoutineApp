using RoutineApp.Data.Entities;
using RoutineApp.Mappers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoutineApp.Mappers.Classes
{
    public class ImageMapper : IImageMapper
    {
        public List<string> MapBitsToImages(List<Image> images)
        {
            if (images.Count == 0 || images is null) return null;

            List<string> imagesList = new();

            foreach (var image in images)
            {
                string imageBase64Data = Convert.ToBase64String(image.Img);

                imagesList.Add(string.Format("data:image/jpg;base64,{0}", imageBase64Data));
            }

            return imagesList;
        }
    }
}
