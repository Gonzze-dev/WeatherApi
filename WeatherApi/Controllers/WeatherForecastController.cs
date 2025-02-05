using Microsoft.AspNetCore.Mvc;
using WeatherApi.Interfaces;
using WeatherApi.Repository;

namespace WeatherApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController(
        [FromKeyedServices("ApiRepository")]
        IRepositoryWeatherForecast apiRepository,
        [FromKeyedServices("RedisCacheRepository")]
        IRepositoryWeatherForecast redisCacheRepository
    ) : Controller
    {
        readonly IRepositoryWeatherForecast _apiRepository = apiRepository;
        readonly IRepositoryWeatherForecast _redisCacheRepository = redisCacheRepository;

        [HttpGet("GetWeatherForecast")]
        public async Task<IActionResult> GetWeatherForecastData()
        {
            //try
            //{
            //    var result = await _apiRepository.GetWeatherForecastData();

            //    return Ok(result);
            //}
            //catch (Exception ex)
            //{
            //    return StatusCode(500, ex.Message);
            //}

            try
            {
                var result = await _redisCacheRepository.GetWeatherForecastData();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
