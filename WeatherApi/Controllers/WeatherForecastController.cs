using Microsoft.AspNetCore.Mvc;
using WeatherApi.Filters;
using WeatherApi.Services;

namespace WeatherApi.Controllers
{

    [WeatherForecastExceptionFilter]
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController(
        WeatherForecastService weatherForecastService
    ) : Controller
    {
        readonly WeatherForecastService _weatherForecastService = weatherForecastService;

        [HttpGet("GetWeatherForecast")]
        public async Task<IActionResult> GetWeatherForecastData()
        {
            var result = await _weatherForecastService.GetWeatherForecastData();

            return Ok(result);
        }
    }
}
