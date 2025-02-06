namespace WeatherApi.Interfaces
{
    public interface IRepositoryWeatherForecast<T>
    {
        Task<T> GetWeatherForecastData();
    }
}
