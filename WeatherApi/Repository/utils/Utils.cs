using StackExchange.Redis;
using System.Text.Json;

namespace WeatherApi.Repository.utils
{
    public static class Utils
    {
        public static bool ValidatedJson(string json)
        {
            try
            {
                using JsonDocument doc = JsonDocument.Parse(json);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
