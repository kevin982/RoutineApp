using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using ExerciseMS_Core.Models.Requests;
using ExerciseMS_Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration Configuration;

        public ImageService(ILogger<ImageService> logger, IConfiguration configuration)
        {
            _logger = logger;
            Configuration = configuration;
        }


        public async Task<string> UploadImageAsync(IFormFile image)
        {
            try
            {
                return await Upload(new UploadImageRequest
                {
                    Image = image.OpenReadStream(),
                    ApiKey = Configuration["Cloudinary:apikey"],
                    Cloud = Configuration["Cloudinary:cloud"],
                    Secret = Configuration["Cloudinary:secret"],
                    ImageName = image.FileName
                });
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
        private async Task<string> Upload(UploadImageRequest model)
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
