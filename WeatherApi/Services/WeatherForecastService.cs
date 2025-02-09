
using StackExchange.Redis;
using WeatherApi.Helpers;
using WeatherApi.Interfaces;
using WeatherApi.Repository;

namespace WeatherApi.Services
{
    public class WeatherForecastService
    (
        [FromKeyedServices("ApiRepository")]
        IRepositoryWeatherForecast<HttpResponseMessage?> apiRepository,
        RedisCacheRepository redisCacheRepository,
        WeatherForecastRepositoryHelper weatherForecastRepositoryHelper
    )
    {

        readonly IRepositoryWeatherForecast<HttpResponseMessage?> _apiRepository = apiRepository;
        readonly RedisCacheRepository _redisCacheRepository = redisCacheRepository;
        readonly WeatherForecastRepositoryHelper _weatherForecastRepositoryHelper = weatherForecastRepositoryHelper;

        public async Task<object?> GetWeatherForecastData()
        {
            RedisValue resultRedis = await _redisCacheRepository.GetWeatherForecastData();
            object? result;

            if (!resultRedis.IsNullOrEmpty)
            {
                result = _weatherForecastRepositoryHelper.ResponseHelper(resultRedis);


                return result;
            }

            HttpResponseMessage? resultApi = await _apiRepository.GetWeatherForecastData();

            result = await _weatherForecastRepositoryHelper.ResponseHelper(resultApi);

            _ = _redisCacheRepository.Save(result); //fire and forget!

            return result;
        }
    }
}
