using Microsoft.AspNetCore.Mvc;
using WeatherApi.Interfaces;
using WeatherApi.Repository;
using WeatherApi.Service;

namespace WeatherApi.Controllers
{

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
            try
            {
                var result = await _weatherForecastService.GetWeatherForecastData();

                if (_weatherForecastService.Validate())
                    BadRequest(_weatherForecastService.Errors);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

           
        }
    }
}
