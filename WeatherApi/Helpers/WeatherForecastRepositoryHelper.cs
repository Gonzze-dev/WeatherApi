using System.Text.Json;
using StackExchange.Redis;
using WeatherApi.Repository.utils;

namespace WeatherApi.Helpers
{
    public class WeatherForecastRepositoryHelper
    {

        public object? ResponseHelper(RedisValue redisResult) //RedisResponse
        {
            if (redisResult.IsNullOrEmpty)
                throw new BadHttpRequestException("Error to get data from Redis DB");


            var redisData = redisResult.HasValue ? redisResult.ToString() : string.Empty;

            var isJsonValid = Utils.ValidatedJson(redisData);

            if (!isJsonValid)
                throw new BadHttpRequestException("Error invalid fromat of json");

            var result = JsonSerializer.Deserialize<object>(redisData);

            if (result == null)
                throw new BadHttpRequestException("Error: deserialization returned null");

            return result;
        }
        public async Task<object> ResponseHelper(HttpResponseMessage? req) //ApiResponse
        {
            if (req == null)
                throw new BadHttpRequestException("Error, request is null");

            if (!req.IsSuccessStatusCode)
                throw new BadHttpRequestException("Error to try get data of weather forecast api");

            var content = await req.Content.ReadAsStringAsync();

            var contentJson = JsonSerializer.Deserialize<object>(content);

            if (contentJson == null)
                throw new BadHttpRequestException("Error, API response a invalid JSON");

            return contentJson;
        }

    }
}
