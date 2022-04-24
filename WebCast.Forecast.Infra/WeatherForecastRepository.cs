using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebCast.Forecast.Domain.Forecast.Entities;
using WebCast.Forecast.Domain.Forecast.Interfaces.Infra;

namespace WebCast.Forecast.Infra
{
    public class WeatherForecastRepository : IWeatherForecastRepository
    {
        private readonly IDistributedCache _repository;
        public WeatherForecastRepository(IDistributedCache repository)
        {
            _repository = repository;
        }
        public async Task AddForecastAsync(WeatherForecast weatherForecast)
        {
            var options = new DistributedCacheEntryOptions()
            {
                AbsoluteExpiration =  weatherForecast.Date.AddTicks(new TimeOnly(23, 59, 59).Ticks)
            };
            var weatherForecastSerliazed = JsonSerializer.Serialize(weatherForecast);

            await _repository.SetStringAsync(GetCacheKeyByWheatherForecastDate(weatherForecast.Date) , weatherForecastSerliazed, options);
        }

        public async Task<IEnumerable<WeatherForecast>> GetNextSevenDaysWeatherForecastsAsync()
        {
            var weatherForecasts = new WeatherForecast[7];
            var nextSevenDaysWeatherForecastsTasks = new Task[7];
            var actualDate = DateTime.Now;
            for (int i = 0; i < 7; i++)
            {
                nextSevenDaysWeatherForecastsTasks[i] = GetWeatherForecastAsync(actualDate.AddDays(i));
            }
            await Task.WhenAll(nextSevenDaysWeatherForecastsTasks);

            for (int i = 0; i < nextSevenDaysWeatherForecastsTasks.Length; i++)
            {
                weatherForecasts[i] = ((Task<WeatherForecast>)nextSevenDaysWeatherForecastsTasks[i]).Result;
            }
            return weatherForecasts;
        }
        private async Task<WeatherForecast> GetWeatherForecastAsync(DateTime date)
        {
            var weatherForecast = await _repository.GetStringAsync(GetCacheKeyByWheatherForecastDate(date));
            if (string.IsNullOrEmpty(weatherForecast))
                return null;
            var result = JsonSerializer.Deserialize<WeatherForecastDTO>(weatherForecast);

            return new WeatherForecast(result.Temperature, result.Date);
        }
        private string GetCacheKeyByWheatherForecastDate(DateTime forecastDate)
        {
            return forecastDate.ToShortDateString();
        }
    }
}
