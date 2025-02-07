using System.Text.Json;
using RedisDB;
using StackExchange.Redis;
using WeatherApi.Interfaces;
using WeatherApi.Repository.utils;

namespace WeatherApi.Repository
{
    public class RedisCacheRepository(
        RedisDBConnection redisDB
        ) : IRepositoryWeatherForecast<RedisValue>
    {
        readonly IDatabase _DBConnection = redisDB.Connection.GetDatabase();

        public async Task<RedisValue> GetWeatherForecastData()
        {
            string keyName = "WeatherForecastData";
            var redisResult = await _DBConnection.StringGetAsync(keyName);

            
            return redisResult;
        }

        public async Task Save(object Data, string keyName = "WeatherForecastData")
        {
            var expire = TimeSpan.FromHours(1);
            await _DBConnection.StringSetAsync(keyName, JsonSerializer.Serialize(Data), expire);
        }

    }
}