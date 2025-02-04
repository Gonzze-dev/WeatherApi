using WeatherApi.Interfaces;

namespace WeatherApi.Repository
{
    public class RedisCacheRepository : IRepositoryWeatherForecast
    {
        public async Task<object> GetWeatherForecastData()
        {
            throw new NotImplementedException();
        }

        public async Task Save()
        { 
            throw new NotImplementedException(); 
        }

    }
}
