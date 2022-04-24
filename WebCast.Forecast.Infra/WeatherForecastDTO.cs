using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCast.Forecast.Domain.Forecast.Entities;

namespace WebCast.Forecast.Infra
{
    public class WeatherForecastDTO
    {
        public double Temperature { get; set; }
        public DateTime Date { get; set; }

        public WeatherForecastDTO()
        {

        }
        public WeatherForecastDTO(WeatherForecast weatherForecast)
        {
            Temperature = weatherForecast.Temperature;
            Date = weatherForecast.Date;
        }
    }
}
