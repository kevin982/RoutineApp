using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using ExerciseMS_Core.Models.Requests;
using ExerciseMS_Core.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseMS_Infraestructure.Services
{
    public class ImageService : IImageService
    {
        private readonly ILogger<ImageService> _logger;

        public ImageService(ILogger<ImageService> logger)
        {
            _logger = logger;
        }

        public async Task<string> UploadImageAsync(UploadImageRequest model)
        {

            Account ac = new(model.Cloud, model.ApiKey, model.Secret);

            Cloudinary c = new(ac);

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(model.ImageName, model.Image),
                Folder = "RoutineApp",
            };
            var uploadResult = await c.UploadAsync(uploadParams);

            return uploadResult.SecureUrl.ToString();
 
        }
    }
}
