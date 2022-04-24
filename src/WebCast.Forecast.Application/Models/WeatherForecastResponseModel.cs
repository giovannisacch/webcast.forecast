using WebCast.Forecast.Domain.Forecast.Entities;

namespace WebCast.Forecast.Application.Models
{
    public class WeatherForecastResponseModel
    {
        public double Temperature { get; set; }
        public DateTime Date { get; set; }
        public string Summary { get; set; }

        public WeatherForecastResponseModel(WeatherForecast weatherForecast)
        {
            if(weatherForecast == null)
            {
                Summary = "NOT_PREDICTED";
                return;
            }
            Temperature = weatherForecast.Temperature;
            Date = weatherForecast.Date;
            Summary = weatherForecast.GetTemperatureSummary();
        }
    }
}
