using WebCast.Forecast.Domain.Forecast.Entities;

namespace WebCast.Forecast.Domain.Forecast.Interfaces.Infra
{
    public interface IWeatherForecastRepository
    {
        Task<IEnumerable<WeatherForecast>> GetNextSevenDaysWeatherForecastsAsync();
        Task AddForecastAsync(WeatherForecast weatherForecast);
    }
}
