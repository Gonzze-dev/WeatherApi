using System.Text.Json;
using StackExchange.Redis;
using WeatherApi.Repository;
using WeatherApi.Repository.utils;

namespace WeatherApi.Helpers
{
    public class WeatherForecastRepositoryHelper
    {
        public readonly List<string> Errors = [];
        public object? ResponseHelper(RedisValue redisResult) //RedisResponse
        {
            if (redisResult.IsNullOrEmpty)
            {
                Errors.Add("Error to get data from Redis DB");
                return null;
            }

            var redisData = redisResult.HasValue ? redisResult.ToString() : string.Empty;

            var isJsonValid = Utils.ValidatedJson(redisData);

            if (!isJsonValid)
            {
                Errors.Add("Error invalid fromat of json");
                return null;
            }
            

            var result = JsonSerializer.Deserialize<object>(redisData);

            if (result == null)
            {
                Errors.Add("Error: deserialization returned null");
                return null;
            }

            return result;
        }
        public async Task<object?> ResponseHelper(HttpResponseMessage? req) //ApiResponse
        {
            if (req == null)
            {
                Errors.Add("Error, request is null");
                return null;
            }

            if (!req.IsSuccessStatusCode)
            {
                Errors.Add("Error al intentar obtener los datos");
                return null;
            }

            var content = await req.Content.ReadAsStringAsync();

            var contentJson = JsonSerializer.Deserialize<object>(content);

            if (contentJson == null)
            {
                Errors.Add("Error, API response a invalid JSON");
                return null;
            }

            return contentJson;
        }

        public bool Validate()
        {
            return Errors.Count > 0;
        }
    }
}
