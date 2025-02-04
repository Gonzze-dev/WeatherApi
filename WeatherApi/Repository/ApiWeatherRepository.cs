
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using WeatherApi.Interfaces;

namespace WeatherApi.Repository
{
    public class ApiRepository(
            IConfiguration configuration,
            HttpClient httpClient
            ) : IRepositoryWeatherForecast
    {
        readonly IConfiguration _configuration = configuration;
        readonly HttpClient _httpClient = httpClient;

        public async Task<object> GetWeatherForecastData()
        {
            var BASE_API = _configuration["BASEURL_API"];
            var TOKEN = _configuration["WEATHER_API_TOKEN"];

            var location = "Gualeguaychu";

            var reqUrl = $"{location}?unitGroup=metric&include=days%2Chours%2Calerts%2Ccurrent&key={TOKEN}&contentType=json";

            var reqConfig = new HttpRequestMessage(HttpMethod.Get, $"{BASE_API}/{reqUrl}")
            {
                Headers =
                {
                    { "Accept", "application/json" },
                }
            };

            var req = await _httpClient.SendAsync(reqConfig);

            if (!req.IsSuccessStatusCode)
                throw new Exception($"Error al intentar obtener los datos");

            var content = await req.Content.ReadAsStringAsync();

            var contentJson = JsonSerializer.Deserialize<object>(content);

            if (contentJson == null)
                throw new Exception("Error, API response a invalid JSON");

            return contentJson;
        }

    }
}
