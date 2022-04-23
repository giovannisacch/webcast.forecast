using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCast.Forecast.Domain.Forecast.Entities;

namespace WebCast.Forecast.Domain.Forecast.Interfaces.Infra
{
    public interface IWeatherForecastRepository
    {
        Task<List<WeatherForecast>> GetNextSevenDaysWeatherForecastsAsync();
        Task AddForecastAsync(WeatherForecast weatherForecast);
    }
}
