using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCast.Forecast.Domain.Common.CustomExceptions;

namespace WebCast.Forecast.Domain.Forecast.Entities
{
    public class WeatherForecast
    {
        public double Temperature { get; private set; }
        public DateOnly Date { get; private set; }

        private static readonly Dictionary<double, string> MaxTemperaturesPerSummaryCrescentOrdered = new Dictionary<double, string>()
        {
            {-10, "Freezing"}, {5, "Bracing"}, {10, "Chilly"}, {14, "Cool"}, {18, "Mild"}, {22, "Warm"}, {26, "Balmy"}, {32, "Hot"}, {42, "Sweltering"}, {60, "Scorching"}
        };
        public WeatherForecast()
        {

        }

        public WeatherForecast(int temperature, DateOnly date)
        {
            SetTemperature(temperature);
            SetDate(date);
        }
        public void SetDate(DateOnly date)
        {
            var actualDate = DateOnly.FromDateTime(DateTime.Now.Date);
            if (actualDate.CompareTo(date) > 0)
                throw new DomainLogicException(ErrorConstants.PAST_FOREACAST_DATE);
            Date = date;

        }
        public void SetTemperature(double temperature)
        {
            if (-60 < temperature || 60 > temperature)
                throw new DomainLogicException(ErrorConstants.INVALID_TEMPERATURE);
            Temperature = temperature;
        }
        public string GetTemperatureSummary()
        {
            return Temperature.ToString();
        }
        public override string ToString()
        {
            for (int i = 0; i < MaxTemperaturesPerSummaryCrescentOrdered.Count; i++)
            {
                var currentElement = MaxTemperaturesPerSummaryCrescentOrdered.ElementAt(i);
                if(Temperature <= currentElement.Key)
                    return currentElement.Value;
            }
            return "Invalid Weather";
        }
    }
}
