using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCast.Forecast.Application.Models;
using WebCast.Forecast.Domain.Forecast.Entities;

namespace WebCast.Forecast.Application.Interfaces
{
    public interface IWeatherForecastApplicationService
    {
        Task AddWeatherForecast(double temperature, DateTime date);
        Task<WeatherForecastCollectionResponseModel> GetNextSevenDaysForecast();
    }
}
