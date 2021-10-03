using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace MVCRoutineAppClient.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public StatisticsService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<string> GetExerciseStatisticsAsync(string accessToken, Guid exerciseId, int month, int year)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("Ocelot");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                
                var response = await client.GetAsync($"/v1/Statistics/{exerciseId}/{month}/{year}");

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