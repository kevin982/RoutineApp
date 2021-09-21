using MVCRoutineAppClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MVCRoutineAppClient.Services
{
    public class RoutineService : IRoutineService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RoutineService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<string> AddExerciseToRoutineAsync(string accessToken, AddExerciseToRoutineRequest request)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("Ocelot");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

                var response = await client.PostAsync("/v1/Routine", content);

                string result = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(result)) return result;

                var error = new
                {
                    statusCode = (int)response.StatusCode,
                    title = response.ReasonPhrase,
                    succeeded = false
                };

                return JsonSerializer.Serialize(error);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string> RemoveExerciseFromRoutineAsync(string accessToken, Guid exerciseId)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("Ocelot");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var response = await client.DeleteAsync($"/v1/Routine/{exerciseId}");

                string result = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(result)) return result;

                var error = new
                {
                    statusCode = (int)response.StatusCode,
                    title = response.ReasonPhrase,
                    succeeded = false
                };

                return JsonSerializer.Serialize(error);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
