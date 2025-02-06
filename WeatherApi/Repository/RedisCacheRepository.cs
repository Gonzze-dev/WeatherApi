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
            var redisResult = await _DBConnection.StringGetAsync("PrimeraData");

            
            return redisResult;
        }

        public async Task Save()
        { 
            throw new NotImplementedException(); 
        }

    }
}
