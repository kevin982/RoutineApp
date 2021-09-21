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
        public async Task<JObject> CreateExerciseAsync(CreateExerciseRequestModel model, string accessToken)
        {
            try
            {
                if (!model.Image.IsImage())
                {
                    JObject e = new();

                    e.Add("statusCode", 400);
                    e.Add("title", "The file must be an image");
                    e.Add("succeeded", false);

                    return e;
                }


                var content = new MultipartFormDataContent();

                content.Add(new StreamContent(model.Image.OpenReadStream()), "Image", "Image");
                
                content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data") { Name = "Image", FileName = model.Image.FileName };

                content.Add(new StringContent(model.Category), "CategoryId");
                content.Add(new StringContent(model.Name), "Name");
                content.Add(new StringContent(model.FileContentType), "FileContentType");

                var client = _httpClientFactory.CreateClient("Ocelot");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var result = await client.PostAsync("/v1/Exercise", content);

                string bodyContent = await result.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(bodyContent)) return JObject.Parse(bodyContent);

                JObject error = new();

                error.Add("statusCode", (int)result.StatusCode);
                error.Add("title", result.ReasonPhrase);
                error.Add("succeeded", false);

                return error;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string> GetExercisesByCategory(string accessToken, Guid categoryId, int index, int size)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("Ocelot");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var response = await client.GetAsync($"/v1/Exercise/Category/{categoryId}/{index}/{size}");

                string content = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(content)) return content;

                var error = new 
                {
                    statusCode = (int)response.StatusCode,
                    title = response.ReasonPhrase,
                    succeeded = false
                };

                return JsonConvert.SerializeObject(error);

            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public async Task<string> GetIndexesCount(string accessToken, Guid categoryId, int size)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("Ocelot");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var response = await client.GetAsync($"/v1/Exercise/IndexesCount/{categoryId}/{size}");

                string content = await response.Content.ReadAsStringAsync();

                if(!string.IsNullOrEmpty(content)) return content;

                var errorMessage = new 
                {
                    statusCode = (int)response.StatusCode,  
                    title = response.ReasonPhrase,
                    succeeded = false
                };

                return JsonConvert.SerializeObject(errorMessage);
            }
            catch (Exception)
            {
                throw;
            }

        }

    }
}
