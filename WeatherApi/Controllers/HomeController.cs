using System;
using Microsoft.AspNetCore.Mvc;
using static System.Net.WebRequestMethods;

namespace WeatherApi.Controllers
{
    
    public class HomeController : Controller
    {
        readonly IConfiguration _configEnv;
        readonly HttpClient _httpClient;
        public HomeController(
            IConfiguration configuration,
            HttpClient httpClient
            )
        {
            _configEnv = configuration;
            _httpClient = httpClient;
        }

        [HttpGet("GetWeatherForecast")]
        public async Task<IActionResult> GetWeatherForecastData()
        {
            var BASE_API = _configEnv["BASEURL_API"];
            var TOKEN = _configEnv["WEATHER_API_TOKEN"];
            
            var location = "Gualeguaychu";

            var reqUrl = $"{location}?unitGroup=metric&include=days%2Chours%2Calerts%2Ccurrent&key={TOKEN}&contentType=json";

            var reqConfig = new HttpRequestMessage(HttpMethod.Get, $"{BASE_API}/{reqUrl}")
            {
                Headers =
                {
                    { "Accept", "application/json" },
                }
            };

            var req = await _httpClient.SendAsync( reqConfig );

            var content = req.Content;

            if (!req.IsSuccessStatusCode)
                return StatusCode((int) req.StatusCode , "Error, al obtener los datos");

            var result = await req.Content.ReadAsStringAsync();

            return Ok(result);
        }
    }
}
