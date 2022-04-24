using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCast.Forecast.Domain.Forecast.Entities;

namespace WebCast.Forecast.Application.Models
{
    public class WeatherForecastCollectionResponseModel
    {
        public IEnumerable<WeatherForecastResponseModel> Forecasts { get; set; }
        public WeatherForecastCollectionResponseModel(IEnumerable<WeatherForecast> weatherForecasts)
        {
            Forecasts = weatherForecasts.Select(x => new WeatherForecastResponseModel(x));
        }
    }
}
