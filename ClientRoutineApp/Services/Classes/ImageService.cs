using ClientRoutineApp.Models;
using ClientRoutineApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace ClientRoutineApp.Services.Classes
{
    public class ImageService : IImageService
    {
 
        public async Task<string> PostImageAsync(PostImageRequestModel model)
        {
            Account ac = new(model.Cloud, model.ApiKey, model.Secret);

            Cloudinary c = new(ac);

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(model.ImageName, model.Image),
            };
            var uploadResult = await c.UploadAsync(uploadParams);

            return uploadResult.SecureUrl.ToString();

        }
    }
}
