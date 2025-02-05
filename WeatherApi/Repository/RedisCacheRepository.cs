using System.Text.Json;
using RedisDB;
using StackExchange.Redis;
using WeatherApi.Interfaces;
using WeatherApi.Repository.utils;

namespace WeatherApi.Repository
{
    public class RedisCacheRepository(
        RedisDBConnection redisDB
        ) : IRepositoryWeatherForecast
    {
        readonly IDatabase _DBConnection = redisDB.Connection.GetDatabase();

        public async Task<object> GetWeatherForecastData()
        {
            var redisResult = await _DBConnection.StringGetAsync("PrimeraData");

            if (redisResult.IsNullOrEmpty)
                throw new Exception("Error to get data from Redis DB");

            var redisData = redisResult.HasValue ? redisResult.ToString() : string.Empty;

            var isJsonValid = Utils.ValidatedJson(redisData);

            if (!isJsonValid)
                throw new Exception("Error invalid fromat of json");

            var result = JsonSerializer.Deserialize<object>(redisData);

            if (result == null)
                throw new Exception("Error: deserialization returned null");

            return result;
        }

        public async Task Save()
        { 
            throw new NotImplementedException(); 
        }

    }
}
