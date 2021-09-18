using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MVCRoutineAppClient.Extensions;
using MVCRoutineAppClient.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MVCRoutineAppClient.Services
{
    public class ExerciseService : IExerciseService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<ExerciseService> _logger;

        public ExerciseService(IHttpClientFactory httpClientFactory, ILogger<ExerciseService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }
        public async Task<(bool, string)> CreateExerciseAsync(CreateExerciseRequestModel model, string accessToken)
        {
            try
            {
                if (!model.Image.IsImage()) return (false, "The file must be an image!");

                var content = new MultipartFormDataContent();

                content.Add(new StreamContent(model.Image.OpenReadStream()), "Image", "Image");
                
                content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data") { Name = "Image", FileName = model.Image.FileName };

                content.Add(new StringContent(model.Category), "CategoryId");
                content.Add(new StringContent(model.Name), "Name");
                content.Add(new StringContent(model.FileContentType), "FileContentType");

                var client = _httpClientFactory.CreateClient("Ocelot");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var result = await client.PostAsync("/v1/Exercise", content);

                if (result.IsSuccessStatusCode) return (true, "The exercise has been created!");

                string bodyContent = await result.Content.ReadAsStringAsync();

                if (string.IsNullOrEmpty(bodyContent)) return (false, "We could not create the exercise");

                var response = JObject.Parse(bodyContent);

                return (false, response["title"].ToString());
            }
            catch (Exception)
            {
                throw;
            }
        }
 
    }
}
