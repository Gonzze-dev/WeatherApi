using System.Text.Json;
using WeatherApi.Interfaces;

namespace WeatherApi.Repository
{
   public class ApiRepository : IRepositoryWeatherForecast<HttpResponseMessage?>
    {
        readonly IConfiguration _configuration;
        readonly HttpClient _httpClient;

        readonly string BASE_API;
        readonly string TOKEN;

        public ApiRepository(
            IConfiguration configuration,
            HttpClient httpClient
        )
        {
            _configuration = configuration;
            _httpClient = httpClient;

            BASE_API = _configuration["BASEURL_API"] ?? "";
            TOKEN = _configuration["WEATHER_API_TOKEN"] ?? "";
        }

        public async Task<HttpResponseMessage?> GetWeatherForecastData()
        {
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

            return req;
        }

    }
}
