namespace WeatherApi.Interfaces
{
    public interface IRepositoryWeatherForecast
    {
        Task<object> GetWeatherForecastData();
    }
}
