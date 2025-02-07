
using StackExchange.Redis;
using WeatherApi.Helpers;
using WeatherApi.Interfaces;
using WeatherApi.Repository;

namespace WeatherApi.Service
{
    public class WeatherForecastService
    (
        [FromKeyedServices("ApiRepository")]
        IRepositoryWeatherForecast<HttpResponseMessage?> apiRepository,
        RedisCacheRepository redisCacheRepository,
        WeatherForecastRepositoryHelper weatherForecastRepositoryHelper
    )
    {
        public readonly List<string> Errors = [];

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

                if (_weatherForecastRepositoryHelper.Validate())
                {
                    Errors.AddRange(_weatherForecastRepositoryHelper.Errors);
                    return null;
                }

                return result;

            }

            HttpResponseMessage? resultApi = await _apiRepository.GetWeatherForecastData();

            result = await _weatherForecastRepositoryHelper.ResponseHelper(resultApi);

            if (_weatherForecastRepositoryHelper.Validate())
            {
                Errors.AddRange(_weatherForecastRepositoryHelper.Errors);
                return null;
            }

            _redisCacheRepository.Save(result);

            return result;
        }

        public bool Validate()
        {
            return Errors.Count > 0;
        }
    }
}
