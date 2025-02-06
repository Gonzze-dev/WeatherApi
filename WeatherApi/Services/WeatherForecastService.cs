
using StackExchange.Redis;
using WeatherApi.Helpers;
using WeatherApi.Interfaces;

namespace WeatherApi.Service
{
    public class WeatherForecastService
    (
        [FromKeyedServices("ApiRepository")]
        IRepositoryWeatherForecast<HttpResponseMessage?> apiRepository,
        [FromKeyedServices("RedisCacheRepository")]
        IRepositoryWeatherForecast<RedisValue> redisCacheRepository,
        WeatherForecastRepositoryHelper weatherForecastRepositoryHelper
    )
    {
        public readonly List<string> Errors = [];

        readonly IRepositoryWeatherForecast<HttpResponseMessage?> _apiRepository = apiRepository;
        readonly IRepositoryWeatherForecast<RedisValue> _redisCacheRepository = redisCacheRepository;
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

                Console.WriteLine(result);
                return result;

            }

            HttpResponseMessage? resultApi = await _apiRepository.GetWeatherForecastData();

            result = await _weatherForecastRepositoryHelper.ResponseHelper(resultApi);

            if (_weatherForecastRepositoryHelper.Validate())
            {
                Errors.AddRange(_weatherForecastRepositoryHelper.Errors);
                return null;
            }

            return result;
        }

        public bool Validate()
        {
            return Errors.Count > 0;
        }
    }
}
