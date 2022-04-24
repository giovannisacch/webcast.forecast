using WebCast.Forecast.Application.Interfaces;
using WebCast.Forecast.Application.Models;
using WebCast.Forecast.Domain.Forecast.Entities;
using WebCast.Forecast.Domain.Forecast.Interfaces.Infra;

namespace WebCast.Forecast.Application.Services
{
    public class WeatherForecastApplicationService : IWeatherForecastApplicationService
    {
        private IWeatherForecastRepository _weatherForecastRepository;
        public WeatherForecastApplicationService(IWeatherForecastRepository weatherForecastRepository)
        {
            _weatherForecastRepository = weatherForecastRepository;
        }

        public async Task AddWeatherForecast(double temperature, DateTime date)
        {
            var weatherForecast = new WeatherForecast(temperature, date);
            await _weatherForecastRepository.AddForecastAsync(weatherForecast);
        }

        public async Task<WeatherForecastCollectionResponseModel> GetNextSevenDaysForecast()
        {
            var forecasts = await _weatherForecastRepository.GetNextSevenDaysWeatherForecastsAsync();
            var response = new WeatherForecastCollectionResponseModel(forecasts);
            return response;
        }
    }
}
