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
                AbsoluteExpiration = weatherForecast.Date.ToDateTime(new TimeOnly(23, 59, 59))
            };
            
            var weatherForecastSerliazed = JsonSerializer.Serialize(weatherForecast);

            await _repository.SetStringAsync(GetCacheKeyByWheatherForecastDate(weatherForecast.Date) , weatherForecastSerliazed, options);
        }

        public async Task<List<WeatherForecast>> GetNextSevenDaysWeatherForecastsAsync()
        {
            var weatherForecasts = new List<WeatherForecast>(7);
            var nextSevenDaysWeatherForecastsTasks = new Task[7];
            var actualDate = DateOnly.FromDateTime(DateTime.Now);
            for (int i = 0; i < 7; i++)
            {
                nextSevenDaysWeatherForecastsTasks[i] = GetWeatherForecastAsync(actualDate);
            }
            await Task.WhenAll(nextSevenDaysWeatherForecastsTasks);

            for (int i = 0; i < nextSevenDaysWeatherForecastsTasks.Length; i++)
            {
                weatherForecasts.Add(((Task<WeatherForecast>)nextSevenDaysWeatherForecastsTasks[i]).Result);
            }
            return weatherForecasts;
        }
        private async Task<WeatherForecast> GetWeatherForecastAsync(DateOnly date)
        {
            var weatherForecast = await _repository.GetStringAsync(GetCacheKeyByWheatherForecastDate(date));
            if (string.IsNullOrEmpty(weatherForecast))
                return null;
            return JsonSerializer.Deserialize<WeatherForecast>(weatherForecast);
        }
        private string GetCacheKeyByWheatherForecastDate(DateOnly forecastDate)
        {
            return forecastDate.ToShortDateString();
        }
    }
}
