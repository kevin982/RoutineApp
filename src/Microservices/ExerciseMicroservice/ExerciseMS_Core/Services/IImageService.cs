using ExerciseMS_Core.Models.Requests;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseMS_Core.Services
{
    public interface IImageService
    {
        Task<string> UploadImageAsync(UploadImageRequest model); 
    }
}
